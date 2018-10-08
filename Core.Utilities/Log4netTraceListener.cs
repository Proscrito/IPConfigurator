using System.Diagnostics;

namespace Core.Utilities
{
    public class Log4netTraceListener : TraceListener
    {
        private readonly log4net.ILog _log;

        public Log4netTraceListener()
        {
            _log = log4net.LogManager.GetLogger("TraceLogger");
        }

        public Log4netTraceListener(log4net.ILog log)
        {
            _log = log;
        }

        public override void Write(string message)
        {
            _log?.Info(message);
        }

        public override void WriteLine(string message)
        {
            _log?.Info(message);
        }
    }
}
