using Infrastructure.Clients;
using Refit;
using WebApp.Providers;

namespace WebApp.Extensions
{
	public static class BuildExtension
	{
		public static void AddRefitClients(this WebApplicationBuilder builder)
		{
			builder.Services
				.AddRefitClient<IWeatherApiClient>()
				.ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["Clients:WeatherApi:BasePath"]!));
				//.AddHttpMessageHandler<RefitAuthHandler>();

            builder.Services
                .AddRefitClient<IAuthApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["Clients:WeatherApi:BasePath"]!));
		}

	}
}
