using System.Text.RegularExpressions;

namespace Codecept2SeleniumTranspiler
{
    public static class JavaScriptToPythonStepConverter
    {
        public static string Convert(string jsCode)
        {
            var lines = Regex.Split(jsCode, @"(?<=;)\s*").Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
            var replacedLines = new List<string>();

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();

                if (!trimmedLine.StartsWith("I."))
                {
                    var cleanedAwaitAndSemicolonLine = Regex.Replace(trimmedLine, @"^await\s+|\s*;+$", "");
                    replacedLines.Add(cleanedAwaitAndSemicolonLine);
                    continue;
                }

                var match = Regex.Match(trimmedLine, @"I\.(\w+)\((.*)\);?");
                if (!match.Success)
                {
                    replacedLines.Add("# Hatalı satır: " + line);
                    continue;
                }

                var jsMethod = match.Groups[1].Value;
                var parameters = match.Groups[2].Value;

                if (!StepMappings.JsToPython.TryGetValue(jsMethod, out var pyMethod))
                {
                    replacedLines.Add($"# Bilinmeyen metod: I.{jsMethod}({parameters})");
                    continue;
                }

                replacedLines.Add($"self.{pyMethod}({parameters})");
            }

            return string.Join(Environment.NewLine, replacedLines);
        }
    }
}