using LanguageTranslator.ClassLibrary;
using LanguageTranslator.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace LanguageTranslator.WebApp.Controllers
{
    public class SiteController : Controller
    {
        private readonly ILogger<SiteController> _logger;
        private readonly IStringLocalizer<SiteController> _localizer;

        public SiteController(ILogger<SiteController> logger, IStringLocalizer<SiteController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            string? formProcessed = Request.Query["btnSubmit"];

            if (formProcessed != null && formProcessed.ToLower() == "translate")
            {
                string testTextInput = "Testing the language translator app.";
                string testTextLanguage = "es";
                string? translationInput = Request.Query["txtTranslationInput"].ToString().Trim();
                string? toLanguage = Request.Query["selToLanguage"].ToString().Trim();
                string? translatorKey = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_KEY");
                string? region = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_REGION");

                if (string.IsNullOrEmpty(translationInput))
                {
                    translationInput = testTextInput;
                    ViewBag.InputText = translationInput;
                } 

                if (string.IsNullOrEmpty(toLanguage))
                {
                    toLanguage = testTextLanguage;
                    ViewBag.ToLanguage = toLanguage;
                }

                if (string.IsNullOrEmpty(translatorKey) || string.IsNullOrEmpty(region))
                {
                    ViewBag.Message = "Please enter your Azure API key and region to use the app.";
                } else
                {
                    LanguageTranslatorApp app = new LanguageTranslatorApp();
                    app.TranslateText(translationInput, toLanguage);
                    ViewBag.TranslatedText = Environment.GetEnvironmentVariable("TRANSLATION"); ;
                    Console.WriteLine($"Translated Text: {app.TranslatedText}. ");
                }

                ViewBag.InputText = translationInput;
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
