using System;
using MarsqaProject.Utilities;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace MarsqaProject.Utilities
{
    public  static class Start
    {      

        
        /// Initializes the logger by configuring it to log to both console and a rolling file.
        /// Logs are stored in the "Logs" directory.
        
        public static void InitializeLogger()
        {
            try
            {
                // Get the base directory of the current domain.
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;

                // Construct the Logs directory path dynamically for better cross-platform compatibility.
                string logDir = Path.Combine(Directory.GetParent(baseDir).Parent.Parent.FullName, "Logs");

                // Ensure the Logs directory exists, create if not.
                Directory.CreateDirectory(logDir);

                // Define the log file path with rolling interval configuration (one file per day).
                string logFilePath = Path.Combine(logDir, "log-.txt");

                // Set up the Serilog logger to write to console and file.
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug() // Adjust based on environment, e.g., Information for production.
                    .WriteTo.Console()
                    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                    .CreateLogger();

                Log.Information("Logging initialized. Log files will be saved to: {LogPath}", logDir);
            }
            catch (Exception ex)
            {
                // Log initialization failed.
                Console.WriteLine("Failed to initialize logger: " + ex.Message);
                throw; // Re-throw the exception after logging it.
            }
        }

        /// Closes and flushes the logger to ensure all logs are written before application shutdown.
        
        public static void CloseAndFlushLogger()
        {
            try
            {
                Log.CloseAndFlush();
            }
            catch (Exception ex)
            {
                // Handle any exceptions during flushing logs.
                Console.WriteLine("Failed to close and flush logger: " + ex.Message);
            }
        }
    }
}
