using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppByCrippy.Logging
{
    internal class Logger
    {
        private static readonly string logFilePath = "error_log.txt";

        public static void LogError(string methodName, Exception ex)
        {
            try
            {
                // Get the directory of the executable
                string directory = AppDomain.CurrentDomain.BaseDirectory;

                // Combine the directory with the log file path
                string logFileFullPath = Path.Combine(directory, logFilePath);

                // Write the error information to the log file
                using (StreamWriter writer = new StreamWriter(logFileFullPath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] Error in {methodName}: {ex.Message}");
                    writer.WriteLine($"StackTrace: {ex.StackTrace}");
                    writer.WriteLine(new string('-', 50)); // Separator for better readability
                }
            }
            catch (Exception logException)
            {
                // If an error occurs while logging, print it to the console
                Console.WriteLine($"Error in Logger.LogError: {logException.Message}");
            }
        }
    }
}
