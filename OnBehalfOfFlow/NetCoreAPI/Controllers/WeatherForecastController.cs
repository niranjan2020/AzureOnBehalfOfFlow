using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly DownStreamAPIService _apiService;

        public JArray DataFromApi { get; set; }

        public JArray DataFromApi2 { get; set; }
        public WeatherForecastController(ILogger<WeatherForecastController> logger, DownStreamAPIService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            string[] scopeRequiredByApi = new string[] { "access_as_user" };
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            DataFromApi = await _apiService.GetApiDataAsync().ConfigureAwait(false);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
