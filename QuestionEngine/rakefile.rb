require_relative 'generic_rakefile.rb'

@coverage_threshold = ENV['coverage_threshold'] || "80"

namespace :testing do

  desc "Performance tests"
    task :performance => :clobber do
      
      @environment_configuration.servers.each do |server| 
        set_host(get_hostname, server)
        set_host("#{get_hostname(with_version: true)}", server)
      end

      errors = []   
          
        cmd = []
        cmd << "\"#{configatron.dir.rake_tools}/apache-jmeter-2.7/bin/jmeter\" -n"
        cmd << "-p \"test/Performance.Tests/support/jmeter.properties\""
        cmd << "-t \"test/Performance.Tests/baseline.jmx\""
        cmd << "-l \"TestResults/JMeter/baseline.jmx.xml\""
        cmd << "-J Path=#{get_hostname(with_version: true)}"
        puts "RUNNING: #{cmd.join(" ")}"
        system cmd.join(" ")
        
        jmxFilename = "TestResults/JMeter/baseline.jmx.xml"
        
        doc = Nokogiri::XML(File.read(jmxFilename))
        xslt  = Nokogiri::XSLT(File.read("test/Performance.Tests/support/jmeter-results-detail-report_21.xsl"))
        File.open("TestResults/JMeter/baseline.jmx.html", "w+") do |f|
            f.write xslt.transform(doc)
        end
        
        allCount = doc.xpath("count(/testResults/*)")
        allTotalTime = doc.xpath("sum(/testResults/*/@t)")
        
        failureCount = doc.xpath("count(/testResults/*[attribute::s='false'])")
        allAverageTime =  allTotalTime / allCount
        
        puts "Average time is #{allAverageTime}ms"
        
        errors << "Performance failure count exceeded for \"#{jmxFilename}\". Actual: #{failureCount} Permissable: 0." unless 0 >= failureCount
        
      raise errors.join("\r\n") unless errors.count == 0
      
  end
  
  desc "Code coverage"
	task :coverage do
		@covreport = 'code-coverage-report'.to_absolute_path
			
		FileUtils.rm_rf @covreport
		FileUtils.mkdir @covreport

		$opencover = "..\\rake-tools\\OpenCover\\OpenCover.Console.exe"
		$nunit_exe = "..\\rake-tools\\nunit\\nunit-console.exe"

	    dlls = []
		dlls << "build\\Keywords.DataAccess.Tests.dll"
		dlls << "build\\Keywords.API.AcceptanceTests.dll"
		dlls << "build\\Keywords.API.dll"
		dlls << "build\\Keywords.API.Test.dll"
		dlls << "build\\Keywords.DataAccess.dll"
		dlls << "build\\Keywords.Domain.dll"
		dlls << "build\\Keywords.Services.dll"
		dlls << "build\\Keywords.API.IntegrationTests.dll"
	    
	    $openCoverCommand = "#{$opencover} -register:user -skipautoprops -target:#{$nunit_exe} -targetargs:\"/noshadow #{dlls.join(" ")} \" -filter:\"+[Keywords*]*\" -output:code-coverage-report\\coveragereport.xml"
		
		sh $openCoverCommand

		sh '..\rake-tools\ReportGenerator\bin\ReportGenerator.exe -reports:code-coverage-report\coveragereport.xml -targetdir:code-coverage-report'

		doc = Nokogiri::HTML(File.read("code-coverage-report\\index.htm"))  
		percentage = doc.xpath("//table[@class='overview'][1]//tr[6]//td").text
		percentage = percentage.sub("%", "" )

		puts "Coverage = #{percentage}%"
		
		if Float(percentage)  < @coverage_threshold.to_f
			fail "\n***** COVERAGE FAIL : *************** The test coverage (#{percentage}%) is below #{@coverage_threshold}% ********************\n\n"
		end
	end  
end
    