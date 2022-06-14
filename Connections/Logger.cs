using System.IO;

namespace supermarket.Connections
{
    internal static class Logger
    {
        public static void Log(string fileName, string text)
        {
            try
            {
                if (!File.Exists($"../{fileName}"))
                {
                    File.Create($"../{fileName}");
                }

                File.AppendAllText($"../{fileName}",
                                "LOGS:\n" +
                                text +
                                "\n\n");
            }
            catch
            {
                // ignored
            }
        }
    }
}
