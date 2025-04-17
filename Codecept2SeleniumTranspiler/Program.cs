using Codecept2SeleniumTranspiler.Helper;
using Codecept2SeleniumTranspiler.Model;

namespace Codecept2SeleniumTranspiler
{
    internal class Program
    {
        private const int ARGS_COUNT = 4;

        private static void Main(string[] args)
        {
            ConsoleHelper.WriteSuccess("Codecept2SeleniumTranspiler - Konsol Uygulaması");
            ConsoleHelper.WriteSuccess("================================================\n");

            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            if (args.Length < ARGS_COUNT)
            {
                Console.WriteLine("Hata: Dosya veya klasör yolu belirtilmedi!");
                ShowHelp();
                return;
            }

            var projectName = args[1];
            var command = args[2].ToLower();
            var pathOrFileName = args[3];

            switch (command)
            {
                case "-f":
                case "--file":
                    //ConvertHelperMethodsWithFile(projectName, pathOrFileName);
                    ConvertUITestMethodsWithFile(projectName, pathOrFileName);
                    break;

                case "-d":
                case "--directory":
                    //ConvertHelperMethodsWithDirectory(projectName, pathOrFileName);
                    ConvertUITestMethodsWithDirectory(projectName, pathOrFileName);
                    break;

                case "-h":
                case "--help":
                    ShowHelp();
                    break;

                default:
                    ConsoleHelper.WriteError($"Bilinmeyen komut: {command}");
                    ShowHelp();
                    break;
            }
        }

        private static void ShowHelp()
        {
            ConsoleHelper.WriteWarning("\nKullanım:");
            ConsoleHelper.WriteWarning("  Codecept2SeleniumTranspiler -p|--projectName -f|--file <dosya_yolu>");
            ConsoleHelper.WriteWarning("  Codecept2SeleniumTranspiler -p|--projectName -d|--directory <klasör_yolu>");
            ConsoleHelper.WriteWarning("  Codecept2SeleniumTranspiler -h|--help");
            ConsoleHelper.WriteWarning("\nParametreler:");
            ConsoleHelper.WriteWarning("  -p, --projectName Proje klasörünü belirtir");
            ConsoleHelper.WriteWarning("  -f, --file        Tek bir JavaScript dosyasını işler");
            ConsoleHelper.WriteWarning("  -d, --directory   Bir klasördeki tüm JavaScript dosyalarını işler");
            ConsoleHelper.WriteWarning("  -h, --help        Bu yardım mesajını gösterir");
        }

        private static void ConvertHelperMethodsWithDirectory(string projectName, string inputFolderPath)
        {
            var jsFiles = Directory.GetFiles(inputFolderPath);

            foreach (string jsFile in jsFiles)
            {
                ConvertHelperMethodsWithFile(projectName, jsFile);
            }
        }

        private static void ConvertHelperMethodsWithFile(string projectName, string jsFilePath)
        {
            ConsoleHelper.WriteInfo($"İşlemdeki dosya: {jsFilePath}");

            var senaryoYardimResult = new List<SenaryoYardim>();

            var jsFileFolderPath = Path.GetDirectoryName(jsFilePath);

            var jsFileMethodList = JavaScriptStepParser.ParseHelperMethods(projectName, jsFilePath);

            //pyhton'a çevir
            foreach (var jsFileMethod in jsFileMethodList)
            {
                ConsoleHelper.WriteInfo($"--JavaScript methodu: {jsFileMethod.Adimlari}");

                var pythonCode = JavaScriptToPythonStepConverter.Convert(jsFileMethod.Adimlari);

                ConsoleHelper.WriteInfo($"--Python methodu: {pythonCode}");

                jsFileMethod.Adimlari = pythonCode;
            }

            senaryoYardimResult.AddRange(jsFileMethodList);

            // json'a yaz
            var jsonFileName = string.Concat(Path.GetFileNameWithoutExtension(jsFilePath), ".json");
            var jsonFilePath = Path.Combine(jsFileFolderPath, jsonFileName);
            FileHelper.WriteToJsonFile(jsonFilePath, senaryoYardimResult);

            ConsoleHelper.WriteInfo($"--Json dosyası oluşturuldu: {jsonFilePath}");
        }

        private static void ConvertUITestMethodsWithDirectory(string projectName, string inputFolderPath)
        {
            var jsFiles = Directory.GetFiles(inputFolderPath);

            foreach (string jsFile in jsFiles)
            {
                ConvertUITestMethodsWithFile(projectName, jsFile);
            }
        }

        private static void ConvertUITestMethodsWithFile(string projectName, string jsFilePath)
        {
            ConsoleHelper.WriteInfo($"İşlemdeki dosya: {jsFilePath}");

            var senaryoResult = new List<Senaryo>();

            var jsFileFolderPath = Path.GetDirectoryName(jsFilePath);

            var jsFileMethodList = JavaScriptStepParser.ParseUITestMethods(projectName, jsFilePath);

            //pyhton'a çevir
            foreach (var jsFileMethod in jsFileMethodList)
            {
                ConsoleHelper.WriteInfo("--JavaScript method içeriği:\n");
                ConsoleHelper.WriteInfo(jsFileMethod.Adimlari);

                var adimlariPythonCode = JavaScriptToPythonStepConverter.Convert(jsFileMethod.Adimlari);
                jsFileMethod.Adimlari = adimlariPythonCode;

                ConsoleHelper.WriteInfo("--Python method içeriği:\n");
                ConsoleHelper.WriteInfo(adimlariPythonCode);

                // json'a yaz
                var jsonFileName = string.Concat(jsFileMethod.Isim, ".json");
                var jsonFilePath = Path.Combine(jsFileFolderPath, jsonFileName);
                FileHelper.WriteToJsonFile(jsonFilePath, jsFileMethod);

                ConsoleHelper.WriteSuccess($"--Json dosyası oluşturuldu: {jsonFilePath}\n");
            }
        }
    }
}