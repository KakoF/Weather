using Amazon.DynamoDBv2.DataModel;

namespace Domain.Models
{
	[DynamoDBTable("Favorite")]
	public class FavoriteWeatherModel
	{
		[DynamoDBHashKey]
		public string Id { get; set; } = null!;
		[DynamoDBRangeKey]
		public string UserId { get; set; } = null!;
		[DynamoDBProperty]
		public WeatherModel Favorite { get; set; } = null!;

	}
}
