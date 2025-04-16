using Codecept2SeleniumTranspiler.Helper;
using Codecept2SeleniumTranspiler.Model;

namespace Codecept2SeleniumTranspiler
{
    internal class Program
    {
        public static Guid ProjeId { get; set; } = Guid.NewGuid();

        private static void Main(string[] args)
        {
            Console.WriteLine("Codecept2SeleniumTranspiler - Konsol Uygulaması");
            Console.WriteLine("================================================");

            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            string command = args[0].ToLower();

            switch (command)
            {
                case "-f":
                case "--file":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Hata: Dosya yolu belirtilmedi!");
                        ShowHelp();
                        return;
                    }
                    ConvertUITestMethodsWithFile(args[1]);
                    break;

                case "-d":
                case "--directory":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Hata: Klasör yolu belirtilmedi!");
                        ShowHelp();
                        return;
                    }
                    ConvertUITestMethodsWithDirectory(args[1]);
                    break;

                case "-h":
                case "--help":
                    ShowHelp();
                    break;

                default:
                    Console.WriteLine($"Bilinmeyen komut: {command}");
                    ShowHelp();
                    break;
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("\nKullanım:");
            Console.WriteLine("  Codecept2SeleniumTranspiler -f|--file <dosya_yolu>");
            Console.WriteLine("  Codecept2SeleniumTranspiler -d|--directory <klasör_yolu>");
            Console.WriteLine("  Codecept2SeleniumTranspiler -h|--help");
            Console.WriteLine("\nParametreler:");
            Console.WriteLine("  -f, --file        Tek bir JavaScript dosyasını işler");
            Console.WriteLine("  -d, --directory   Bir klasördeki tüm JavaScript dosyalarını işler");
            Console.WriteLine("  -h, --help        Bu yardım mesajını gösterir");
        }

        private static void ConvertHelperMethods(string inputFolderPath)
        {
            var jsFiles = Directory.GetFiles(inputFolderPath);

            foreach (string jsFile in jsFiles)
            {
                ConsoleHelper.WriteInfo($"İşlemdeki dosya: {jsFile}");

                var senaryoYardimResult = new List<SenaryoYardim>();

                var jsFileContent = FileHelper.ReadFile(jsFile);
                var jsFileMethodList = JavaScriptStepParser.ParseHelperMethods(jsFileContent, ProjeId);

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
                var jsonFileName = Path.GetFileNameWithoutExtension(jsFile) + ".json";
                var jsonFilePath = Path.Combine(inputFolderPath, jsonFileName);
                FileHelper.WriteToJsonFile(jsonFilePath, senaryoYardimResult);

                ConsoleHelper.WriteInfo($"--Json dosyası oluşturuldu: {jsonFilePath}");
            }
        }

        private static void ConvertUITestMethodsWithDirectory(string inputFolderPath)
        {
            var jsFiles = Directory.GetFiles(inputFolderPath);

            foreach (string jsFile in jsFiles)
            {
                ConvertUITestMethodsWithFile(jsFile);
            }
        }

        private static void ConvertUITestMethodsWithFile(string jsFilePath)
        {
            ConsoleHelper.WriteInfo($"İşlemdeki dosya: {jsFilePath}");

            var senaryoResult = new List<Senaryo>();

            var jsFileFolderPath = Path.GetDirectoryName(jsFilePath);
            var jsFileContent = FileHelper.ReadFile(jsFilePath);
            var jsFileMethodList = JavaScriptStepParser.ParseUITestMethods(jsFileContent, ProjeId, jsFileFolderPath);

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