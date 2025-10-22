using LanguageTranslator.ClassLibrary;
using LanguageTranslator.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;

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
            string formProcessed = Request.Query["btnSubmit"];

            if (formProcessed != null && formProcessed.ToLower() == "translate")
            {
                string testTextInput = "Testing the language translator app.";
                string testTextLanguage = "es";
                string translationInput = Request.Query["txtTranslationInput"];
                string toLanguage = Request.Query["selToLanguage"];

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

                /*
                string languageFile = "C:\\Code\\GitHub\\Language-Translator\\Solution\\LanguageTranslator.WebApp\\languages.json";
                string jsonString = File().ReadAllText(languageFile);

                Language languageData = JsonSerializer.Deserialize<Language>(jsonString);

                OnDemandTranslator translator = new OnDemandTranslator("934yh6hy23ye45e45hweviohv98ye45y6e78945hwww698", "eastus");
                string translatedText = await translator.TranslateTextAsync(translationInput, toLanguage);

                if (translatedText != null)
                {
                    Console.WriteLine($"Original: {translationInput}");
                    Console.WriteLine($"Translated ({toLanguage}): {translatedText}");
                }
                */

                ViewBag.InputText = translationInput;
                ViewBag.TranslatedText = "Probando la aplicación traductor de idiomas.";

                ViewBag.TestTranslation = _localizer["lightweight_language_translator_web_tool"].Value;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
