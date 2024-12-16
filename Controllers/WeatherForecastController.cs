using Microsoft.AspNetCore.Mvc;

namespace restapi.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
            {
                return BadRequest("Forecast data is required.");
            }

            // Bạn có thể xử lý dữ liệu ở đây, ví dụ lưu vào database hoặc thực hiện các công việc khác
            _logger.LogInformation("Received forecast: Date={Date}, Temperature={TemperatureC}, Summary={Summary}",
                forecast.Date, forecast.TemperatureC, forecast.Summary);

            // Trả về mã trạng thái 201 (Created) và thông tin dự báo vừa nhận
            return CreatedAtAction(nameof(Get), new { id = forecast.Date }, forecast);
        }
    }

}

