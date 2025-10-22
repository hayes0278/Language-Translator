using Azure;
using Azure.AI.Translation.Text;

namespace LanguageTranslator.ClassLibrary
{
    public class LanguageTranslator
    {
        #region fields

        private string _testMe;

        #endregion

        #region constructors

        public LanguageTranslator()
        {
            _testMe = "Testing";
        }

        #endregion

        #region public methods
        public static void TranslateText(string inputText, string targetLanguage)
        {
            string translatorKey = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_KEY");
            string region = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_REGION");

            OnDemandTranslator translator = new OnDemandTranslator(translatorKey, region);
            translator.TranslateTextAsync(inputText, targetLanguage);
        }
        #endregion

        #region private methods

        private static void MyPrivateMethod()
        {

        }

        #endregion

        #region properties

        public string TestMe
        {
            get { return _testMe; }
            set { _testMe = value; }
        }

        #endregion

        #region deconstructors
        #endregion
    }
}
