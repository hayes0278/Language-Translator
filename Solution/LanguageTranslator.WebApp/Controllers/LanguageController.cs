using LanguageTranslator.ClassLibrary;
using Microsoft.AspNetCore.Mvc;

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

        public LanguageController(ILogger<LanguageController> logger)
        {
            _logger = logger;
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
