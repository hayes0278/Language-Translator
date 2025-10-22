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

        [HttpGet(Name = "GetLanguageTranslation")]
        public IEnumerable<Language> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Language
            {
                Abbreviation = Summaries[Random.Shared.Next(Summaries.Length)],
                Name = Summaries[Random.Shared.Next(Summaries.Length)],
                NativeName = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
