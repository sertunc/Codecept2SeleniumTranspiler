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
            { "waitToHide", "GizlenmesiniBekle" },
            { "waitForVisible", "GorunmesiniBekle" },            
            { "click", "Tikla" },
            { "fillField", "Yaz" },
            { "checkOption", "Tikla" },

            { "moveCursorTo", "ImleciGotur" },
            { "setVatanComboBox", "Tikla" },
            { "selectVatanTab", "Tikla" },
            { "pressKey", "Yaz" },
        };
    }
}