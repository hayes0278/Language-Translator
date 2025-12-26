using Azure;
using Azure.AI.Translation.Text;

namespace LanguageTranslator.ClassLibrary
{
    public class LanguageTranslatorApp
    {
        #region fields

        private Language[] _languageList;
        private int _languageCount = 0;
        private string _translatedText = null;

        #endregion

        #region constructors
        #endregion

        #region public methods
        public void TranslateText(string inputText, string targetLanguage)
        {
            Console.WriteLine($"Main thread started. Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            Task backgroundTask = Task.Run(() =>
            {
                string? translatorKey = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_KEY");
                string? region = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_REGION");

                Console.WriteLine($"Translation task started. Thread ID: {Thread.CurrentThread.ManagedThreadId}");

                OnDemandTranslator translator = new OnDemandTranslator(translatorKey, region);      
                translator.TranslateTextAsync(inputText, targetLanguage);
                _translatedText = translator.TranslatedText;

                Console.WriteLine("Translation task finished.");
            });

            backgroundTask.Wait();
        }

        public void GetLanguageList ()
        {
            // Replace with your actual Translator service key and endpoint
            string key = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_KEY");
            string endpoint = "https://api.cognitive.microsofttranslator.com/";
            string region = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_REGION");

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(key))
            {
                Console.WriteLine("Please enter your Azure translator key and region to use the app.");
                return;
            }

            var client = new TextTranslationClient(new AzureKeyCredential(key), region);

            try
            {
                Response<GetSupportedLanguagesResult> response = client.GetSupportedLanguages(cancellationToken: CancellationToken.None);
                GetSupportedLanguagesResult languages = response.Value;

                Console.WriteLine($"Number of supported languages for translate operations: {languages.Translation.Count}.");
                _languageCount = languages.Translation.Count;

                Console.WriteLine("Supported languages:");

                foreach (var language in languages.Translation)
                {
                    Language appLanguage = new Language();
                    appLanguage.Name = language.Value.Name;
                    appLanguage.Direction = language.Value.Directionality.ToString();
                    appLanguage.Name = language.Key;
                    _languageList.Append(appLanguage);

                    Console.WriteLine($"- Code: {language.Key}, Name: {language.Value.Name}, Direction: {language.Value.Directionality}");
                }
            }
            catch (RequestFailedException exception)
            {
                Console.WriteLine($"Error Code: {exception.ErrorCode}");
                Console.WriteLine($"Message: {exception.Message}");
            }
        }
        #endregion

        #region private methods

        private static void MyPrivateMethod()
        {

        }

        #endregion

        #region properties

        public Language[] LanguageList
        {
            get { return _languageList; }
            set { _languageList = value; }
        }

        public string TranslatedText
        {
            get { return _translatedText; }
            set { _translatedText = value; }
        }

        #endregion

        #region deconstructors
        #endregion
    }
}
