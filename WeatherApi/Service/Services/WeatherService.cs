using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Infrastructure.Clients;

namespace Service.Services
{
	public class WeatherService : IWeatherService
	{
		private readonly IOpenWeatherMapClient _client;
		public WeatherService(IOpenWeatherMapClient client)
		{
			_client = client;
		}
		public async Task<WeatherModel> GetAsync(string location)
		{
			try
			{
				var weather = await _client.GetWeatherAsync(location);
				return weather;
			}
			catch (Exception ex) {
				throw new DomainException(ex.Message, 400);
			}
		}
	}
}
