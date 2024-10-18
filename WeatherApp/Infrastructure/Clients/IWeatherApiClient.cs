using Domain.Models;
using Refit;

namespace Infrastructure.Clients
{
	public interface IWeatherApiClient
	{
		[Get("/Weather/Location/{location}")]
		Task<WeatherModel> GetLocationAsync(string location);

		[Get("/FavoriteWeather")]
		Task<IEnumerable<FavoriteWeatherModel>> GetFavoriteAsync([Header("Authorization")] string token);

		[Post("/FavoriteWeather")]
		Task<FavoriteWeatherModel> PostFavoriteAsync([Body(BodySerializationMethod.Serialized)] string location, [Header("Authorization")] string token);

		[Delete("/FavoriteWeather/{id}")]
		Task<bool> DeleteFavoriteAsync([AliasAs("id")] string id, [Header("Authorization")] string token);

	}
}
