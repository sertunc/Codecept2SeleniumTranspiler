using Newtonsoft.Json;

namespace Codecept2SeleniumTranspiler.Helper
{
    public static class FileHelper
    {
        public static string ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            return File.ReadAllText(filePath);
        }

        public static void WriteToJsonFile(string filePath, object objects)
        {
            try
            {
                var json = JsonConvert.SerializeObject(objects, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                throw new IOException($"An error occurred while writing to the file: {filePath}", ex);
            }
        }

        public static void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}