namespace Codecept2SeleniumTranspiler
{
    public static class StepMappings
    {
        public static readonly Dictionary<string, string> JsToPython = new()
        {
            { "I", "self" },
            { "amOnPage", "Git" },
            { "see", "SayfadaVarMi" },
            { "wait", "Bekle" },
            { "waitToHide", "BekleVeGizle" },
            { "waitForVisible", "BekleVeGoster" },
            { "click", "Tikla" },
            { "fillField", "Yaz" },
            { "checkOption", "Tikla" },
        };
    }
}