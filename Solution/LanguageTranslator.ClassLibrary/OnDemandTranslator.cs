using System;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.AI.Translation.Text;

namespace LanguageTranslator.ClassLibrary
{
    public class OnDemandTranslator
    {
        private readonly TextTranslationClient _client;
        private readonly string _region;

        public OnDemandTranslator(string translatorKey, string region)
        {
            _region = region;
            var credential = new AzureKeyCredential(translatorKey);
            _client = new TextTranslationClient(credential, region);
        }

        public async Task<string> TranslateTextAsync(string textToTranslate, string targetLanguage)
        {
            try
            {
                Response<IReadOnlyList<TranslatedTextItem>> response = await _client.TranslateAsync(targetLanguage, textToTranslate);
                TranslatedTextItem translation = response.Value.FirstOrDefault();
                return translation?.Translations?.FirstOrDefault()?.Text;
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Error translating text: {ex.Message}");
                return null;
            }
        }

        public static async Task Main(string[] args)
        {
            // Replace with your actual key and region
            string translatorKey = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_KEY");
            string region = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_REGION");

            if (string.IsNullOrEmpty(translatorKey) || string.IsNullOrEmpty(region))
            {
                Console.WriteLine("Please set AZURE_TRANSLATOR_KEY and AZURE_TRANSLATOR_REGION environment variables.");
                return;
            }

            var translator = new OnDemandTranslator(translatorKey, region);

            string originalText = "Hello, how are you?";
            string targetLang = "es"; // Spanish

            string translatedText = await translator.TranslateTextAsync(originalText, targetLang);

            if (translatedText != null)
            {
                Console.WriteLine($"Original: {originalText}");
                Console.WriteLine($"Translated ({targetLang}): {translatedText}");
            }
        }
    }
}
