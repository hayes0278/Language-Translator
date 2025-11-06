using LanguageTranslator.ClassLibrary;
using static System.Net.Mime.MediaTypeNames;

namespace LanguageTranslator.NunitTests
{
    public class LanguageTranslator_Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TranslateTextAsync_Test()
        {
            string translatorKey = "";
            string region = "";

            string testText = "Hello, how are you?";
            string language = "es";
            string translatedText = string.Empty;

            OnDemandTranslator translator = new OnDemandTranslator(translatorKey, region);
            translator.TranslateTextAsync(testText, language);
            translatedText = translator.TranslatedText;
            if (!string.IsNullOrEmpty(translatedText)) { Assert.Pass(); } else { Assert.Fail(); }
        }

        [Test]
        public void Test2()
        {
            Assert.Pass();
        }

        [Test]
        public void Test3()
        {
            Assert.Pass();
        }

        [Test]
        public void Test4()
        {
            Assert.Pass();
        }
    }
}