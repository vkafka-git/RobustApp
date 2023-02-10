using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.LoggerService
{
    public class Logger : ILogger
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json",optional: false, reloadOnChange: true)
            .Build();

        public void LogInformation(string information)
        {
            var customLoggingLevel = Configuration.GetSection("CustomLoggingLevel");
            var levelSwitch = new Serilog.Core.LoggingLevelSwitch();
            levelSwitch.MinimumLevel = getLogEventLevel(customLoggingLevel.Value);
            var logLevel = getLogEventLevel(LogEventLevel.Information.ToString());

            using (var log = GetLog(levelSwitch))
            {
                if (log.IsEnabled(logLevel))
                {
                    log.Write(logLevel, information);
                }
                //else
                //{
                //    var result = $"Logger: eventLevel:{nameof(logLevel)} is not enabled";
                //    System.Diagnostics.Debug.WriteLine(result);
                //}
            }

            //throw new NotImplementedException();
        }

        public void LogError(string information, Exception ex)
        {
            var customLoggingLevel = Configuration.GetSection("CustomLoggingLevel");
            var levelSwitch = new Serilog.Core.LoggingLevelSwitch();
            levelSwitch.MinimumLevel = getLogEventLevel(customLoggingLevel.Value);
            var logLevel = getLogEventLevel(LogEventLevel.Error.ToString());

            using (var log = GetLog(levelSwitch))
            {
                if (log.IsEnabled(LogEventLevel.Error))
                {
                    if (ex != null)
                    {
                        log.Write(logLevel, ex, information);
                    }
                    else
                    {
                        log.Write(logLevel, information);
                    }
                }
                else
                {
                    var result = $"Logger: eventLevel:{nameof(logLevel)} is not enabled";
                    System.Diagnostics.Debug.WriteLine(result);
                }
            }

            //throw new NotImplementedException();
        }

        private LogEventLevel getLogEventLevel(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return LogEventLevel.Error;
            }

            return GetEnumFilter<LogEventLevel>(value);
        }

        private static T GetEnumFilter<T>(string value) where T : struct, Enum =>
                Enum.TryParse(value, out T result) ? result : throw new Exception("Invalid value");


        private static Serilog.Core.Logger GetLog(Serilog.Core.LoggingLevelSwitch levelSwitch)
        {
            return new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                //.MinimumLevel.Override()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        /*
        public void LogInformation(LogLevels eventLevel, string information, Exception ex = null, params object[] values)
        {
            var customLoggingLevel = Configuration.GetSection("CustomLoggingLevel");
            var levelSwitch = new Serilog.Core.LoggingLevelSwitch();
            levelSwitch.MinimumLevel = getLogEventLevel(customLoggingLevel.Value);
            var logLevel = getLogEventLevel(eventLevel.ToString());

            using(var log = GetLog(levelSwitch))
            {
                if (log.IsEnabled(logLevel))
                {
                    if(ex != null)
                    {
                        log.Write(logLevel,ex,information,values);
                    }
                    else
                    {
                        log.Write(logLevel,information,values);
                    }
                }
                else
                {
                    var result = $"Logger: eventLevel:{nameof(logLevel)} is not enabled";
                    System.Diagnostics.Debug.WriteLine(result);
                }
            }

            //throw new NotImplementedException();
        }
        */
    }
}
