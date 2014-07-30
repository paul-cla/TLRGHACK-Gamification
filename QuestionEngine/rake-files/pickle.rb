class Object
  define_method :not do
    Not.new(self)
  end

  class Not
    private *instance_methods.select { |m| m !~ /(^__|^\W|^binding$)/ }

    def initialize(subject)
      @subject = subject
    end

    def method_missing(sym, *args, &blk)
      !@subject.send(sym,*args,&blk)
    end
  end
end

module Pickled
  def pickle(name, config, *args)
    args || args = []
    args.insert 0, name
    body = proc {
      puts "Pickling Features..."
      cmd = "#{config.command} -f #{config.folder} -o LivingDoc -df #{config.format}"
      p cmd
      system cmd
      log = IO.read(config.log)
      if log.not.empty?
        puts "ERRORS Encountered... please review the log below:\r\n"
        puts log
        fail
      end
      puts "Pickled."
      puts "Copying to #{config.destination.server}"
      `xcopy /E /H /Y /I LivingDoc \\\\#{config.destination.server}\\#{config.destination.share}\\#{config.destination.path}`
    }

    Rake::Task.define_task(*args, &body) unless config.nil?
  end
end