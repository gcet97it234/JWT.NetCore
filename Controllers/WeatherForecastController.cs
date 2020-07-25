using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Utilities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace JWT.NetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly UserManager<UserInformation> userManager;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,UserManager<UserInformation> userManager)
        {
            _logger = logger;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {

            HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues values);
            if(values.Any())
            {
                var t = values.First();
                var t1 = t.Split(" ");
                var result = JWTManager.ValidateCurrentToken(t1[1]);
            }

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
