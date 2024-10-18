using System.Text.Json.Serialization;

namespace Domain.Models
{
	public class WeatherModel
	{
		public List<Weather>? Weather { get; set; }
		public string? @Base { get; set; }
		public Main? Main { get; set; }
		public int? Visibility { get; set; }
		public Wind? Wind { get; set; }
		public Clouds? Clouds { get; set; }
		public int? Dt { get; set; }
		public Sys? Sys { get; set; }
		public int? Timezone { get; set; }
		public int? Id { get; set; }
		public string? Name { get; set; }
		public int? Cod { get; set; }


	}

	public class Clouds
	{
		public int? All { get; set; }
	}

	public class Main
	{
		public double? Temp { get; set; }
		[JsonPropertyName("feels_like")]
		public double? FeelsLike { get; set; }
		[JsonPropertyName("temp_min")]
		public double? TempMin { get; set; }
		[JsonPropertyName("temp_max")]
		public double? TempMax { get; set; }
		public int? Pressure { get; set; }
		public int? Humidity { get; set; }
		[JsonPropertyName("sea_level")]
		public int? SeaLevel { get; set; }
		[JsonPropertyName("grnd_level")]
		public int? GrndLevel { get; set; }
	}

	public class Sys
	{
		public string? Country { get; set; }
		public int? Sunrise { get; set; }
		public int? Sunset { get; set; }
	}

	public class Weather
	{
		public int? Id { get; set; }
		public string? Main { get; set; }
		public string? Description { get; set; }
		public string? Icon { get; set; }
	}

	public class Wind
	{
		public double? Dpeed { get; set; }
		public int? Deg { get; set; }
		public double? Gust { get; set; }
	}
}
