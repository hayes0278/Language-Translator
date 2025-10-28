using Azure;
using Azure.AI.Translation.Text;

namespace LanguageTranslator.ClassLibrary
{
    public class LanguageTranslatorApp
    {
        #region fields

        private string _testMe;

        #endregion

        #region constructors

        public LanguageTranslatorApp()
        {
            _testMe = "Testing";
        }

        #endregion

        #region public methods
        public void TranslateText(string inputText, string targetLanguage)
        {
            Console.WriteLine($"Main thread started. Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            Task backgroundTask = Task.Run(() =>
            {
                string translatorKey = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_KEY");
                string region = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_REGION");

                Console.WriteLine($"Translation task started. Thread ID: {Thread.CurrentThread.ManagedThreadId}");

                OnDemandTranslator translator = new OnDemandTranslator(translatorKey, region);      
                translator.TranslateTextAsync(inputText, targetLanguage);

                Console.WriteLine("Translation task finished.");
            });

            backgroundTask.Wait();
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
