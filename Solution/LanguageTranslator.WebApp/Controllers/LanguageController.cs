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

        [HttpGet(Name = "GetLanguageTranslation")]
        public IEnumerable<Language> Get()
        {
            LanguageTranslatorApp translatorApp = new LanguageTranslatorApp();
            translatorApp.GetLanguageList();

            return Enumerable.Range(1, 5).Select(index => new Language
            {
                Abbreviation = Summaries[Random.Shared.Next(Summaries.Length)],
                Name = Summaries[Random.Shared.Next(Summaries.Length)],
                NativeName = Summaries[Random.Shared.Next(Summaries.Length)],
                Direction = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
