using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using ILogger = Robust.LoggerService.ILogger;

//using System.Net.Http;
//using System.Web.Http;

namespace Robust.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        //private readonly ILogger<WeatherForecastController> logger;
        private readonly ILogger _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> _logger)
        public WeatherForecastController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            HttpResponseMessage response1;
            try
            {
                var response = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();

                if (response == null)
                {
                    //response1 = new HttpResponseMessage(HttpStatusCode.NotFound);
                    //response1.ReasonPhrase = "Employee not found";
                    //return BadRequest(response);
                    var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("We cannot use IDs greater than 100.")
                    };
                    //throw new HttpResponseException(message);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response1 = new HttpResponseMessage(HttpStatusCode.NoContent);
                response1.ReasonPhrase = "Employee not found";
                return BadRequest(response1);
            }

        }

        [Route("CheckId/{id}")]
        [HttpGet]
        //[MyException]
        //[UnhandledExceptionFilter]
        public IActionResult CheckId(int id)
        {
            _logger.LogInformation("CheckId message..............");
            //throw new InvalidMovieException("Invalid Movie Id");
            if (id > 100)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "We cannot use IDs greater than 100."
                };
                return BadRequest(message);
            }
            return Ok(id);
           
        }

        // /api/values2/divide/1/2
        [HttpGet]
        [Route("divide/{Numerator}/{Denominator}")]
        public IActionResult Divide(double Numerator, double Denominator)
        {
            _logger.LogInformation("Divide message..............");
            //_logger.LogInformation("Divide message..............");
            //if (Denominator == 0)
            //{
            //    return BadRequest();
            //}

            return Ok(Numerator / Denominator);
        }

        // /api/values2 /squareroot/4
        [HttpGet("{radicand}")]
        public IActionResult Squareroot(double radicand)
        {
            if (radicand < 0)
            {
                return BadRequest();
            }

            return Ok(Math.Sqrt(radicand));
        }
    }
}