
namespace Domain.Models
{
	public class FavoriteWeatherModel
	{
		public string Id { get; set; } = null!;
		public string UserId { get; set; } = null!;
		public WeatherModel Favorite { get; set; } = null!;

	}
}
