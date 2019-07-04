using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.API.Test
{
    internal class LogToActionLoggerProvider : ILoggerProvider
    {
        private readonly Action<string> _efCoreLogAction;
        private readonly LogLevel _logLevel;

        public LogToActionLoggerProvider(Action<string> efCoreLogAction, LogLevel logLevel= LogLevel.Information)
        {
            _efCoreLogAction = efCoreLogAction;
            _logLevel = logLevel;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new EFCoreLogger(_efCoreLogAction, _logLevel);
        }

        public void Dispose()
        {
            // NOthing to Dispose
        }
    }
}
