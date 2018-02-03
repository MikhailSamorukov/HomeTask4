using SystemFileWatcher.Abstract;
using System;
using log4net;
using log4net.Config;

namespace SystemFileWatcher
{
    class Logger : ILogger
    {
        private static Logger me;
        private ILog _log = LogManager.GetLogger("LOGGER");

        private Logger()
        {
            XmlConfigurator.Configure();
        }

        public static Logger getLogger()
        {
            if (me == null)
                me = new Logger();
            return me;
        }

        public void LogInfo(string information)
        {
            Console.WriteLine(information);
            _log.Info(information);
        }

        public void LogError(string errorDetails)
        {
            Console.WriteLine(errorDetails);
            _log.Error(errorDetails);
        }
    }
}
