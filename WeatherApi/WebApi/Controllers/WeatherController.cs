using Domain.Models;
using Infrastructure.Clients;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherController : ControllerBase
	{

		private readonly IOpenWeatherMapClient _client;

		public WeatherController(IOpenWeatherMapClient client)
		{
			_client = client;
		}

		[HttpGet]
		[Route("Location/{location}")]
		public async Task<WeatherModel> GetAsync(string location)
		{
			return await _client.GetWeatherAsync(location);
		}
	}
}
