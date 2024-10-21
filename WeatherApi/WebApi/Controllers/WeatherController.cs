using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherController : ControllerBase
	{
		private readonly IWeatherService _service;

		public WeatherController(IWeatherService service)
		{
			_service = service;
		}

		[HttpGet]
		[Route("Location/{location}")]
		public async Task<WeatherModel> GetAsync(string location)
		{
			return await _service.GetAsync(location);
		}
	}
}
