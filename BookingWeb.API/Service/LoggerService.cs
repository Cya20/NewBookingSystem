using BookingWeb.API.Interface;
using Serilog;
using System.Xml;

namespace BookingWeb.API.Service
{
    public class LoggerService : ILoggerService
    {
        private static  NLog.ILogger _logger;

        public LoggerService()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
        }
        public void LogDebug(string message)
        {
            _logger.Debug(message, Formatting.Indented);
        }

        public void LogError(string message)
        {
            _logger.Error(message, Formatting.Indented);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message, Formatting.Indented);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message, Formatting.Indented);
        }
    }
}
