using Domain.Models;
using Refit;

namespace Infrastructure.Clients
{
	public interface IOpenWeatherMapClient
	{
		[Get("/weather")]
		Task<WeatherModel> GetWeatherAsync([AliasAs("q")][Query] string location);
	}
}

