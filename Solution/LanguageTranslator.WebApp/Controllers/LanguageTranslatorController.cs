using LanguageTranslator.ClassLibrary;
using Microsoft.AspNetCore.Mvc;

namespace LanguageTranslator.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanguageTranslatorController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<LanguageTranslatorController> _logger;

        public LanguageTranslatorController(ILogger<LanguageTranslatorController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostLanguageTranslator")]
        public string PostLanguageTranslator(string text, string language)
        {
            text = text.Trim();
            string translatedText = null;
            language = language.Trim();

            try
                {
                Console.WriteLine($"Main thread started. Thread ID: {Thread.CurrentThread.ManagedThreadId}");

                Task backgroundTask = Task.Run(() =>
                {
                    string translatorKey = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_KEY");
                    string region = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_REGION");

                    Console.WriteLine($"Translation task started. Thread ID: {Thread.CurrentThread.ManagedThreadId}");

                    OnDemandTranslator translator = new OnDemandTranslator(translatorKey, region);
                    translator.TranslateTextAsync(text, language);
                    translatedText = translator.TranslatedText;

                    Console.WriteLine("Translation task finished.");
                });

                backgroundTask.Wait();

                return translatedText;
            }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
        }
    }
}
