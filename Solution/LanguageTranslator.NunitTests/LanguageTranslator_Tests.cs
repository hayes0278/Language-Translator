using LanguageTranslator.ClassLibrary;

namespace LanguageTranslator.NunitTests
{
    public class LanguageTranslator_Tests
    {
        string _translatorKey = "";
        string _region = "";
        // also check one in Program.cs

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ODTTranslateTextAsync_Test()
        {
            string testText = "Hello, how are you?";
            string language = "es";
            string translatedText = string.Empty;

            OnDemandTranslator translator = new OnDemandTranslator(_translatorKey, _region);
            translator.TranslateTextAsync(testText, language);
            translatedText = translator.TranslatedText;
            if (!string.IsNullOrEmpty(translatedText)) { Assert.Pass(); } else { Assert.Fail(); }
        }

        [Test]
        public void GetLanguageList_Test()
        {
            LanguageTranslatorApp translatorApp = new LanguageTranslatorApp();
            translatorApp.GetLanguageList();
            var languages = translatorApp.LanguageList;
            if (languages != null) { Assert.Pass(); } else { Assert.Fail(); }
        }

        [Test]
        public void AppTranslateTextAsync_Test()
        {
            string testText = "Hello, how are you?";
            string language = "es";
            string translatedText = string.Empty;

            LanguageTranslatorApp translatorApp = new LanguageTranslatorApp();
            translatorApp.TranslateText(testText, language);
            var languages = translatorApp.LanguageList;
            if (languages != null) { Assert.Pass(); } else { Assert.Fail(); }
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}