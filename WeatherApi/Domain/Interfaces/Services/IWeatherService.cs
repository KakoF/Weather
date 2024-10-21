using Domain.Models;

namespace Domain.Interfaces.Services
{
	public interface IWeatherService
	{
		Task<WeatherModel> GetAsync(string location);
	}
}
