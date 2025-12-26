using LanguageTranslator.ClassLibrary;
using LanguageTranslator.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LanguageTranslator.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanguageController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private string[] _languageList;

        private readonly ILogger<LanguageController> _logger;
        private readonly IStringLocalizer<LanguageController> _localizer;

        public LanguageController(ILogger<LanguageController> logger, IStringLocalizer<LanguageController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        [HttpGet(Name = "GetLanguages")]
        public IEnumerable<Language> Get()
        {
            LanguageTranslatorApp translatorApp = new LanguageTranslatorApp();
            translatorApp.GetLanguageList();
            var languageList = translatorApp.LanguageList;

            return Enumerable.Range(1, 5).Select(index => new Language
            {
                Abbreviation = languageList[index].Abbreviation,
                Name = languageList[index].Name,
                NativeName = languageList[index].NativeName,
                Direction = languageList[index].Direction
            }).ToArray();
        }
    }
}
