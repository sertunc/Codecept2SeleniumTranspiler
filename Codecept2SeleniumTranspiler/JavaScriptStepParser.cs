using Codecept2SeleniumTranspiler.Helper;
using Codecept2SeleniumTranspiler.Model;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Codecept2SeleniumTranspiler
{
    public static class JavaScriptStepParser
    {
        public static List<SenaryoYardim> ParseHelperMethods(string projeId, string jsFilePath)
        {
            var result = new List<SenaryoYardim>();

            var jsFileName = Path.GetFileNameWithoutExtension(jsFilePath);
            var jsFileContent = FileHelper.ReadFile(jsFilePath);

            var methodRegex = new Regex(@"async\s+(\w+)\s*\([^)]*\)\s*{([^}]*)}", RegexOptions.Singleline);

            var matches = methodRegex.Matches(jsFileContent);

            foreach (Match match in matches)
            {
                var isim = match.Groups[1].Value.Trim();
                var icerik = match.Groups[2].Value.Trim();

                result.Add(new SenaryoYardim
                {
                    Isim = Clean(isim),
                    Adimlari = icerik,
                    Klasor = Clean(jsFileName),
                    ProjeId = projeId
                });
            }

            return result;
        }

        public static List<Senaryo> ParseUITestMethods(string projeId, string jsFilePath)
        {
            string beforeIcerik = string.Empty;
            var senaryolar = new List<Senaryo>();

            var jsFileContent = FileHelper.ReadFile(jsFilePath);

            // Eğer dosya tamamen yorumsa, direkt geç
            if (Regex.IsMatch(jsFileContent, @"^\s*/\*[\s\S]*\*/\s*$"))
                return senaryolar;

            // Yorum bloklarının konumlarını bul (başlangıç ve bitiş indeksleri)
            var commentBlockMatches = Regex.Matches(jsFileContent, @"/\*[\s\S]*?\*/", RegexOptions.Singleline);
            var commentSpans = commentBlockMatches
                .Cast<Match>()
                .Select(m => (start: m.Index, end: m.Index + m.Length))
                .ToList();

            var beforeRegex = new Regex(@"Before\s*\(\s*async\s*\([^)]+\)\s*=>\s*{([^}]*)}\s*\)\s*;", RegexOptions.Singleline);
            var scenarioPattern = @"Scenario\(['""](?<isim>.*?)['""],\s*async\s*\(\{[^)]*\}\)\s*=>\s*\{\s*(?<icerik>.*?)\s*\}\)\.tag\(['""](?<tag>.*?)['""]\);";
            var scenarioMatches = Regex.Matches(jsFileContent, scenarioPattern, RegexOptions.Singleline);

            // Before bloğu varsa, onu ekle
            var beforeMatch = beforeRegex.Match(jsFileContent);
            if (beforeMatch.Success)
            {
                beforeIcerik = beforeMatch.Groups[1].Value.Trim();
            }

            foreach (Match match in scenarioMatches)
            {
                int scenarioStartIndex = match.Index;

                // Bu senaryo yorum bloklarından birinin içindeyse atla
                bool isInsideComment = commentSpans.Any(span => scenarioStartIndex >= span.start && scenarioStartIndex < span.end);
                if (isInsideComment)
                    continue;

                var isim = match.Groups["isim"].Value.Trim();

                var icerik = string.IsNullOrEmpty(beforeIcerik) ?
                     match.Groups["icerik"].Value.Trim() :
                     string.Concat(beforeIcerik, match.Groups["icerik"].Value.Trim());

                var tag = match.Groups["tag"].Value.Trim();
                var klasor = GetPath(projeId, jsFilePath);

                var senaryo = new Senaryo
                {
                    Isim = Clean(isim),
                    Adimlari = icerik,
                    ScriptId = tag,
                    Klasor = Clean(CleanUITestText(klasor)),
                    ProjeId = projeId,
                };

                senaryolar.Add(senaryo);
            }

            return senaryolar;
        }


        private static string GetPath(string projeId, string jsFilePath)
        {
            var match = Regex.Match(jsFilePath, @$"{Regex.Escape(projeId)}\\(.+)");
            if (!match.Success)
                throw new Exception("Uygun path bulunamadı!");

            var relativePath = match.Groups[1].Value;
            var withoutExtension = Path.Combine(
                Path.GetDirectoryName(relativePath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(relativePath)
            );

            return withoutExtension.Replace("\\", "/");
        }

        private static string Clean(string value)
        {
            // Parantezleri boşlukla değiştir
            string temizlenmis = Regex.Replace(value, "[()]", " ");

            // Alt çizgileri boşlukla değiştir
            temizlenmis = temizlenmis.Replace("_", " ");

            // Fazla boşlukları tek boşluğa indir
            temizlenmis = Regex.Replace(temizlenmis, @"\s+", " ").Trim();

            // Her kelimenin baş harfini büyüt
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            string properCase = ti.ToTitleCase(temizlenmis.ToLower());

            return properCase;
        }

        private static string CleanUITestText(string value)
        {
            if (value.Contains("_ui_test"))
                return value.Replace("_ui_test", "");
            else
                return value;
        }
    }
}