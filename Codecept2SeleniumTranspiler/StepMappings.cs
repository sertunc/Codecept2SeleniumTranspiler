namespace Codecept2SeleniumTranspiler
{
    public static class StepMappings
    {
        public static readonly Dictionary<string, string> JsToPython = new()
        {
            { "I", "self" },
            { "amOnPage", "Git" },
            { "see", "SayfadaVarMi" },
            { "dontSee", "SayfadaYokMu" },
            { "wait", "Bekle" },
            { "waitToHide", "GizlenmesiniBekle" },
            { "waitForVisible", "GorunmesiniBekle" },
            { "waitForInvisible", "KaybolmasiniBekle" },
            { "click", "Tikla" },
            { "fillField", "Yaz" },
            { "checkOption", "Tikla" },
            { "uncheckOption", "SecimiKaldir" },

            //Vatan
            { "moveCursorTo", "ImleciGotur" },
            { "setVatanComboBox", "Tikla" },
            { "selectVatanTab", "Tikla" },
            { "pressKey", "Yaz" },

            //KYSv3
            { "waitUrlEquals", "UrlEsitMi" },

            //KYSv2
            { "waitForElement", "ElementiBekle" },

            //KPSBasvuru, KimlikIzi
            { "scrollPageToBottom", "SayfaAltinaKaydir" },

            //AKST
            { "grabAttributeFrom", "OznitelikGetir" },
            { "say", "Logla" },
            { "waitForEnabled", "EtkinlesmesiniBekle" },
            { "selectOption", "SecenekIsaretle" },
            { "clearField", "Temizle" },
            { "switchToNextTab", "SiradakiSekmeyeGec" },

            //Kimlikizi
            { "seeNumberOfElements", "ElementSayisiEsitMi" },
        };
    }
}