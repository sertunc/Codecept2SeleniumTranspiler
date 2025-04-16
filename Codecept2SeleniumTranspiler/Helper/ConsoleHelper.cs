namespace Codecept2SeleniumTranspiler.Helper
{
    public static class ConsoleHelper
    {
        public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
        }

        public static void WriteError(string message)
        {
            WriteLine(message, ConsoleColor.Red);
        }

        public static void WriteSuccess(string message)
        {
            WriteLine(message, ConsoleColor.Green);
        }

        public static void WriteWarning(string message)
        {
            WriteLine(message, ConsoleColor.Yellow);
        }

        public static void WriteInfo(string message)
        {
            WriteLine(message, ConsoleColor.Cyan);
        }
    }
}