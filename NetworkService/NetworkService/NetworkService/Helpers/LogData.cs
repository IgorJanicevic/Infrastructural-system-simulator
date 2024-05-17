using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Helpers
{
    public class LogData
    {
        private static readonly string logFilePath = "log.txt";

        public static void Log(string message)
        {
            try
            {
                // Koristimo StreamWriter da otvorimo fajl za pisanje
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    // Upisujemo trenutni datum i vreme i poruku u fajl
                    writer.WriteLine($"{DateTime.Now}|{message}");
                }
            }
            catch (Exception ex)
            {
                // Ukoliko dođe do greške prilikom logovanja, ispisujemo je na konzolu
                Console.WriteLine($"Error logging message: {ex.Message}");
            }
        }

        public static string ReadLastLog()
        {
            try
            {
                using (StreamReader reader = new StreamReader(logFilePath))
                {
                    string lastLine = null;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lastLine = line;
                    }
                    return lastLine;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading last log: {ex.Message}");
                return null;
            }
        }
    }
}
