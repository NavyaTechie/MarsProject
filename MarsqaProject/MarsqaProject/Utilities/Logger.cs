using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace MarsqaProject.Utilities
{
    public class Logger
    {
        public static void InitializeLogger()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;

            string logPath = dir.Replace("bin\\Debug\\net6.0", "Logs");

            Log.Information("Logging to file: {LogFile}", logPath);
            Log.Logger = new LoggerConfiguration().
                MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        public static void CloseAndFlushLogger()
        {
            Log.CloseAndFlush(); //Ensure logs are written and flushed
        }
    }
}
