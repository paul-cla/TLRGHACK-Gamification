require 'rubygems'
require 'bundler/setup'
STDOUT.sync=true

require 'zip/zip'
require 'msdeploy'
require 'albacore'
require 'albacore-extensions'
require 'configatron'
require 'erb'
require 'CGI'
require 'rake/clean'
require 'resolv'
require './rake-files/pickle.rb'

configatron.configure_from_hash(YAML::load(File.open("config.env.yml")).to_hash)
configatron.configure_from_hash(YAML::load(File.open("config.featureswitches.yml")).to_hash)

ENV['EnableNuGetPackageRestore'] = 'true'

@environment = ENV['environment'] || 'dev'
@environment_configuration = eval("configatron.env.#{@environment}")
@featureswitches = eval("configatron.features.#{@environment}")

CLOBBER.include(configatron.dir.build_artifacts)
CLOBBER.include(configatron.dir.nuget_package)
CLOBBER.include(configatron.dir.build)
CLOBBER.include(configatron.dir.test_results)

desc "Build solution"
task :build => 'building:build'

desc "Run unit tests (*.Test.dll)"
task :test => ['testing:unit']

desc "Build and run tests"
task :default => [:build, :test]

desc "Setup dev machine"
task :setup => ['config:update', 'utility:add_host_entry', 'building:dev', :build, 'testing:unit']

desc "Create deployment package"
task :package => [:clobber, 'utility:version','utility:generate_declared_parameter_file', :build, :test, 'building:package', 'content:package', 'config:package', 'status_checker:get_package', 'scripts:package'] 

desc  "Deploy the Config and Content to: #{@environment}"
task :deploy => ['utility:generate_parameter_file', 'content:deploy', 'config:deploy', 'status_checker:deploy'] 

task :show_config do
  p "----> ENV: #{@environment}"
  p @environment_configuration
end

task :show_features do
  p "----> ENV: #{@environment}"
  p @featureswitches
end

task :show_all_config do
  p configatron
end


namespace :building do

  desc "Build #{configatron.product} solution for dev"
  msbuild :dev, [:config] => [:clobber] do |msb, args|
    msb.properties.merge! ({
      :configuration => (args[:config] || :debug)
    })
  end

  desc "Build #{configatron.product} solution"
  msbuild :build, [:config] => [:clobber] do |msb, args|
    msb.properties.merge! ({
      :configuration => (args[:config] || :release),
      :outputPath => configatron.dir.build.to_absolute_path,
      :UseWPP_CopyWebApplication => "True"
    })
  end

  desc "Create package of build"
  packager :package => ['utility:ensure_package_folder'] do |pkg|
    pkg.package = 'build-package.zip'.at_path(configatron.dir.build_artifacts)
    pkg.contentPath = configatron.dir.build.to_absolute_path
  end

  desc "Deploy package of build"
  deployer :deploy => 'utility:generate_parameter_file' do |dpy|
    dpy.package = 'build-package.zip'.at_path(configatron.dir.build_artifacts)
    dpy.contentPath = configatron.dir.build.to_absolute_path
    dpy.setParamFile = @parameter_file
    dpy.port = nil
  end

end

namespace :testing do

  desc "Run unit tests (*.Test.dll)"
  nunit :unit => [:clean] do |nunit|
    nunit.working_directory = FileUtils.pwd
    nunit.assemblies = nunit.assemblies = Dir["#{configatron.dir.build}/*.Test.dll"]
    nunit.output = 'UnitTests'
    nunit.projects = []
  end

  task :smoke_tests => ['building:deploy','smoke:run']

  task :warmup_services do
    res = nil
    address = "status.#{@environment_configuration.hostname}"
    1.upto(5) do |x|
      begin
        puts "Attempting (#{x} of 5) to hit #{address}"
        Net::HTTP.start("#{address}") do |http|
          res = http.head('/status')     
        end
        p res
        break if res.code == '200'
      rescue Timeout::Error
        puts "timeout error"
      end
    end
    raise "Deployment failed status check [#{res.code}] - check http://#{address}" unless res.code == '200'

    puts "*** Warmup complete"
  end

  namespace :integration do
   desc "Run integration tests (*.IntegrationTests.dll)"
    nunit :run do |nunit|
      nunit.working_directory = FileUtils.pwd
      nunit.assemblies = nunit.assemblies = Dir["#{configatron.dir.build}/*.IntegrationTests.dll"]
      nunit.output = 'IntegrationTests'
      nunit.projects = []
    end
  end

  namespace :smoke do

    nunit :smoke_tests do |nunit|
      nunit.working_directory = FileUtils.pwd
      nunit.assemblies = Dir["#{configatron.dir.build}/*.SmokeTests.dll"]
      nunit.output = 'SmokeTests'
      nunit.projects = []
    end

    desc "Run smoke tests (*.SmokeTests.dll)"
    task :run do
      @environment_configuration.servers.each do |server| 
        set_host(get_hostname, server)
        set_host("status.#{get_hostname(with_version: false)}", server)
        Rake::Task['testing:smoke:smoke_tests'].execute
      end
    end

  end

  namespace :acceptance do
    specflowreport :specs do |specflow|
      specflow.projects = ["./test/#{configatron.application_name}.AcceptanceTests/#{configatron.application_name}.AcceptanceTests.csproj"]
      specflow.options << "/xmlTestResult:./TestResults/AcceptanceTests-NUnit-Results.xml"
      specflow.options << "/out:./TestResults/#{configatron.application_name}.AcceptanceTests-specs.html"
    end

    specflowreport :steps => :specs  do |specflow|
      specflow.projects = ["./test/#{configatron.application_name}.AcceptanceTests/#{configatron.application_name}.AcceptanceTests.csproj"]
      specflow.options << "/out:./TestResults/#{configatron.application_name}.AcceptanceTests-steps.html"
      specflow.options << "\"/binFolder:#{configatron.dir.build.to_absolute_path}\""
      specflow.report = "stepdefinitionreport"
    end

    nunit :run do |nunit|
      nunit.working_directory = FileUtils.pwd
      nunit.assemblies = [
        "#{configatron.dir.build}/#{configatron.application_name}.AcceptanceTests.dll"]
      nunit.output = 'AcceptanceTests'
    end   
    
    task :pickle => [:living_doc, :wip_doc]
    include Pickled

    desc "Build WiP Documentation"
    pickle :wip_doc, configatron.pickled.wip

    desc "Build Living Documentation"
    pickle :living_doc, configatron.pickled.living_docs

  end
  
  desc "Run acceptance tests"
  task :acceptance_tests do
    @environment_configuration.servers.each { |server| run_acceptance_tests(server) }
  end
  
  def run_acceptance_tests(server)
    Rake::Task['testing:acceptance:run'].reenable
    Rake::Task['testing:acceptance:pickle'].reenable
    Rake::Task['testing:acceptance:steps'].reenable
    begin
      set_host(get_hostname, server)
      set_host("status.#{get_hostname(with_version: false)}", server)
      Rake::Task['testing:acceptance:run'].invoke
      Rake::Task['testing:acceptance:pickle'].invoke
    ensure
      Rake::Task['testing:acceptance:steps'].invoke
    end 
  end

end

namespace :config do

  desc "Create archive of configuration"
  archiver :archive do |arv|
    arv.manifest = 'manifest.xml'
    arv.archiveDir = configatron.dir.archive.to_absolute_path
  end

  desc "Update configuration from archive"
  deployer :update do |dpy|
    dpy.archiveDir = configatron.dir.archive.to_absolute_path
    dpy.app_path = File.join(Dir.pwd, "src", "#{configatron.application_name}.WCF").gsub(File::SEPARATOR, File::ALT_SEPARATOR || File::SEPARATOR)
  end

  desc "Create package of configuration"
   packager :package => ['utility:ensure_package_folder'] do |pkg|
    pkg.package = 'config-management.zip'.at_path(configatron.dir.build_artifacts)
    pkg.archiveDir = configatron.dir.archive.to_absolute_path
  end

  desc "Deploy package of configuration"
  deployer :deploy => 'utility:generate_parameter_file' do |dpy|
    dpy.package = 'config-management.zip'.at_path(configatron.dir.build_artifacts)
    dpy.destinations = @environment_configuration.servers.collect {|s| s[:server]}
    dpy.setParamFile = @parameter_file
  end

end

namespace :content do
  
  desc "Create content package"
  packager :package => ['utility:ensure_package_folder'] do |pkg|
    pkg.package =  "content-package.zip".at_path(configatron.dir.build_artifacts)
    pkg.contentPath = "_PublishedWebsites".at_path(configatron.dir.build)
  end

  desc "Deploy package of content"
  deployer :deploy => 'utility:generate_parameter_file' do |dpy|
    dpy.package = 'content-package.zip'.at_path(configatron.dir.build_artifacts)
    dpy.contentPath = get_deploy_dir
    dpy.destinations = @environment_configuration.servers.collect {|s| s[:server]}
    dpy.setParamFile = @parameter_file
  end

end

namespace :status_checker do

  desc "Deploy StatusChecker"
  task :deploy => [ :deploy_content, :deploy_config ]
  desc "get status checker package from nuget"
  nugetinstall :get_package do |nuget|
    nuget.package = 'Infrastructure.Service.StatusChecker.MsDeployPackage'
    nuget.output_directory = configatron.dir.build_artifacts
    nuget.exclude_version = true
    nuget.sources << "http://nuget.laterooms.com/nuget"
    nuget.command = "#{configatron.dir.rake_tools}/nuget-v2/NuGet.exe"
  end

  deployer :deploy_content => [:get_package, 'utility:generate_parameter_file'] do |dpy|
    dpy.package = 'status-checker-content.zip'.at_path(configatron.dir.statuschecker.package)
    dpy.contentPath = "#{get_deploy_dir(with_version: false)}.StatusChecker"
    dpy.destinations = @environment_configuration.servers.collect {|s| s[:server]}
    dpy.setParamFile = @parameter_file
  end

  deployer :deploy_config => [:get_package, 'utility:generate_parameter_file'] do |dpy|
    dpy.package = 'status-checker-config.zip'.at_path(configatron.dir.statuschecker.package)
    dpy.destinations = @environment_configuration.servers.collect {|s| s[:server]}
    dpy.setParamFile = @parameter_file
  end

end

namespace :nuget do

  desc "Prepare nuget package for API"
  nugetpack :pack => ['utility:version', 'utility:ensure_package_folder'] do |nuget|
    nuget.command = "#{configatron.dir.rake_tools}/nuget-v2/nuget.exe"
    nuget.nuspec = "src/package.nuspec"
    nuget.output = "\"#{configatron.dir.build_artifacts}\""
  end

   desc "Prepare nuget package for API.Events"
  nugetpack :pack_events => ['utility:version', 'utility:ensure_package_folder'] do |nuget|
    nuget.command = "#{configatron.dir.rake_tools}/nuget-v2/nuget.exe"
    nuget.nuspec = "src/#{configatron.application_name}.Events.API/package.nuspec"
    nuget.output = "\"#{configatron.dir.build_artifacts}\""
  end

  desc "Push to nuget for API"
  nugetpush :push => [:pack] do |nuget|
    nuget.command = "#{configatron.dir.rake_tools}/nuget-v2/nuget.exe"
    nuget.package = File.join(configatron.dir.build_artifacts, "#{configatron.application_name}.API.#{@version}.nupkg").gsub(File::SEPARATOR, File::ALT_SEPARATOR || File::SEPARATOR)
    nuget.apikey = 'creat10n'
    nuget.source = "http://nuget.laterooms.com"
  end

   desc "Push to nuget for API.Events"
  nugetpush :push_events => [:pack_events] do |nuget|
    nuget.command = "#{configatron.dir.rake_tools}/nuget-v2/nuget.exe"
    nuget.package = File.join(configatron.dir.build_artifacts, "#{configatron.application_name}.Events.API.#{@version}.nupkg").gsub(File::SEPARATOR, File::ALT_SEPARATOR || File::SEPARATOR)
    nuget.apikey = 'creat10n'
    nuget.source = "http://nuget.laterooms.com"
  end

end

namespace :scripts do

  desc "Package scripts for upstream pipelines"
  task :package do 
    archive = 'scripts.zip'
    puts "Zipping scripts to #{archive}...."
    File.delete archive if File.exists? archive
    Zip::ZipFile.open(archive, Zip::ZipFile::CREATE) do |zipfile|
      Dir["**/{#{configatron.archive.directories.join(',')}}/*"]
        .concat(Dir["{#{configatron.archive.files.join(',')}}"])
        .each do |file|
        puts "   => Adding #{file}..."
        zipfile.add(file, file)
      end
    end  
  end

end

namespace :utility do

  task :ensure_package_folder do
    if File.exists?(configatron.dir.build_artifacts)
      FileUtils.remove_dir(configatron.dir.build_artifacts)
    end

    FileUtils.mkdir(configatron.dir.build_artifacts)
  end

  task :generate_parameter_file => [:get_version] do
    text = File.read("parameters-template.xml.erb".at_path(configatron.dir.parameters))
    @parameter_file = "parameters-#{@environment}.v#{@version}.xml".at_path(configatron.dir.parameters)
    puts "Generating parameter file: #{@parameter_file}..."
    File.open(@parameter_file, "w") do |file|
      file.puts ERB.new(text).result()
    end
  end

   task :generate_declared_parameter_file do
    text = File.read("declared-parameters-template.xml.erb".at_path(configatron.dir.parameters))
    puts "Generating declared-parameters.xml..."
    File.open("declared-parameters.xml".at_path(configatron.dir.parameters), "w") do |file|
      file.puts ERB.new(text).result()
    end
  end

  task :get_version do 
    begin
      @version =  VersionStringBuilder.new.version_string(configatron.version.major, configatron.version.minor)
    rescue
      @version = "#{configatron.version.major}.#{configatron.version.minor}.#{ENV['GO_PIPELINE_LABEL'].dup.insert 2, "."}"
    end
  end

  task :version => [:get_version] do
    AssemblyVersion.new.update(@version)
    
    config = AssemblyVersionConfiguration.new
    AssemblyVersion.new(config).update(@version)
  end

  def set_host(hostname, options = { server: "localhost" })
    ip = Resolv.getaddress options[:server]
    c = `#{configatron.dir.rake_tools}/hosts/hosts.exe set #{hostname} #{ip}`
    if c[0,7] == "[ERROR]"
      c = `#{configatron.dir.rake_tools}/hosts/hosts.exe add #{hostname} #{ip}`
    end
    puts c
  end

  task :add_host_entry do 
    set_host(get_hostname(with_version: false))
    # set_host(@environment_configuration.statuschecker.hostname)
  end
  
  def get_deploy_dir(options = { with_version: true} )
    base_path = File.join(@environment_configuration.deploy_drive, configatron.product, 'Applications', configatron.application_name).gsub(File::SEPARATOR, File::ALT_SEPARATOR || File::SEPARATOR)
    return base_path unless options[:with_version]
    File.join(base_path, "v#{configatron.version.major}.#{configatron.version.minor}").gsub(File::SEPARATOR, File::ALT_SEPARATOR || File::SEPARATOR)
  end

  def get_hostname(options = { with_version: true} )
    return @environment_configuration.hostname unless options[:with_version]
    "v#{configatron.version.major}.#{configatron.version.minor}.#{@environment_configuration.hostname}"
  end


  def escape str
    str.gsub('.', '\\.')
  end

end


Albacore.configure do |config|
  config.tools_check("\"#{configatron.dir.rake_tools}\"")
  config.log_level = :verbose
  config.set_up_tools(configatron.dir.rake_tools)

  config.specflowreport do |specflow|
    specflow.command = "#{configatron.dir.rake_tools}/specflow/SpecFlow_v1.9.0/specflow.exe"
  end

  config.msbuild do |msb|
    msb.command = 'C:/Windows/Microsoft.NET/Framework/v4.0.30319/msbuild.exe'
    msb.solution = "#{configatron.product}.sln"
    msb.properties.merge! ({
      :PipelineDependsOnBuild => "False",
    })
  end

  config.specflowreport do |specflow|
    specflow.command = File.join(configatron.dir.rake_tools, "specflow", "SpecFlow_v1.9.0", "specflow.exe")
  end

  config.deployer do |dpy|
    dpy.command = 'msdeploy.exe'.at_path(configatron.dir.ms_deploy)
    dpy.encryptPassword = 'creat10n'
    dpy.port = @environment_configuration.msdeploy_port
  end

  config.packager do |pkg|
    pkg.command = 'msdeploy.exe'.at_path(configatron.dir.ms_deploy)
    pkg.encryptPassword = 'creat10n'
    pkg.declareParamFile = 'declared-parameters.xml'.at_path(configatron.dir.parameters)
  end

  config.archiver do |arv|
    arv.command = 'msdeploy.exe'.at_path(configatron.dir.ms_deploy)
    arv.encryptPassword = 'creat10n'
    arv.app_name = configatron.application_name
    arv.app_path = File.join(Dir.pwd, "src", "#{configatron.application_name}.WCF").gsub(%r{/}) { "\\" }
  end
end


class AssemblyVersion
 def initialize(config = AssemblyVersionConfiguration.new, svn = SvnVersionWrapper.new, filesystem = FileSystemVersionWrapper.new)
  @svn = svn
    @filesystem = filesystem
    @config = config
 end

 def update(version = nil)
    pattern =/Version\(\"([\d.*]+)\"\)/  

    filelist = @filesystem.dir("#{@config.path_to_version}/**/AssemblyInfo.cs")
    if filelist.size == 0
      raise "no assembly info found"
    end

    projects = @filesystem.dir("#{@config.path_to_version}/**/*.csproj")
    if projects.size != filelist.size
      raise "there is not an AssemblyInfo.cs for each project"
    end

    if version.nil?
      version = VersionStringBuilder.new(@svn,@config.path_to_version).version_string(@config.major_version, @config.minor_version)
    end

    filelist.each do |filepath|
      filecontents = @filesystem.read(filepath)
      @filesystem.write(filepath, String(filecontents).gsub(pattern,"Version(\"#{version}\")"))
    end

    nuspecfiles = @filesystem.dir("#{@config.path_to_version}/**/package.nuspec")
    nuspecfiles.each do |filepath|
      xml = Nokogiri::XML(@filesystem.read(filepath))
      xml.xpath('//xmlns:package/xmlns:metadata/xmlns:version', xml.root.namespaces).each{|v| v.content = version}          
      @filesystem.write(filepath, xml.to_s)
    end
  end

end

class VersionStringBuilder
  def initialize(svn = SvnVersionWrapper.new, path = './src')
    @svn = svn
    @path = path
  end

  def version_string(major, minor, svnrevision = nil)
    svnrevision = svnrevision || @svn.version_of(@path)
    svnDiv = Integer(svnrevision) / 1000
    svnMod = svnrevision[-3,3]
    return "#{major}.#{minor}.#{svnDiv}.#{svnMod}"
  end
end

  class AssemblyVersionConfiguration
   def initialize
      self.path_to_version = './src'
   end

   attr_accessor :path_to_version, :major_version, :minor_version
  end

class SvnVersionWrapper
 def initialize
 end

 def version_of(path)
  svninfo = /([\d]+)/.match(`svnversion`)[0]
 end
end

class FileSystemVersionWrapper
  attr_accessor :events, :files, :filecontent

  def initialize
  end

  def dir(path)
    return Dir[path]
  end  

  def read(filepath)
    return File.read(filepath)
  end

  def write(filepath, content)
    File.open(filepath, "w+") do |file|
      file.puts(content)
    end
  end

end

class StatusChecker
  def generate_endpoints(config, options = { hostname: 'localhost', include_proxy: true } )
    return if config.versions_to_monitor.empty?

    endpoints = []
    hosts_to_monitor = [options[:hostname]] + (config.exists?(:additional_hosts_to_monitor) ? config.additional_hosts_to_monitor : [])

    hosts_to_monitor.each do |host_name|
      config.versions_to_monitor.each do |version_number|
        s = "<add endpoint=\"http://v#{version_number}.#{host_name}/serviceavailability.svc/status\" description=\"#{configatron.application_name} #{version_number} on #{host_name}\""
        s << " proxyAddress=\"127.0.0.1\"" if options[:include_proxy]
        s << " />"
        endpoints << s 
      end
    end
 
    "<serviceEndpoints>#{endpoints.join ''}</serviceEndpoints>"
  end

end