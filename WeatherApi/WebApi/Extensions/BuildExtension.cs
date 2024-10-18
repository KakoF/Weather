using Amazon;
using Amazon.DynamoDBv2;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Infrastructure.Clients;
using Infrastructure.Configs;
using Infrastructure.DbContext;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Refit;
using Service.Services;
using System.Text;

namespace WebApi.Extensions
{
	public static class BuildExtension
	{

		public static void AddConfigurations(this WebApplicationBuilder builder)
		{
			builder.Services.Configure<WeatherConfig>(builder.Configuration.GetSection("Clients:OpenWeatherMap"));
			builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtSettings"));
		}

		public static void AddServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddSingleton<DynamoDbContext>();
			builder.Services.AddSingleton<DynamoDbInitializer>();

			builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
			{
				var config = sp.GetRequiredService<IConfiguration>();
				return new AmazonDynamoDBClient(
					config["AWS:AccessKey"],
					config["AWS:SecretKey"],
					RegionEndpoint.GetBySystemName(config["AWS:Region"])
					);
			});

			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped<IFavoriteService, FavoriteService>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IFavoriteWeatherRepository, FavoriteWeatherRepository>();
		}

		public static void AddAuthentication(this WebApplicationBuilder builder)
		{
			var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtConfig>();
			var key = Encoding.ASCII.GetBytes(jwtSettings!.SecretKey);

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings.Issuer,
					ValidAudience = jwtSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(key)
				};
			});



		}
		public static void AddRefitClients(this WebApplicationBuilder builder)
		{
			builder.Services.AddSingleton<RefitApiKeyHandler>();

			builder.Services
				.AddRefitClient<IOpenWeatherMapClient>()
				.ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["Clients:OpenWeatherMap:BasePath"]!))
				.AddHttpMessageHandler<RefitApiKeyHandler>();
		}

		public static void AddSwaggerGen(this WebApplicationBuilder builder)
		{
			builder.Services.AddSwaggerGen(c =>
			{

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Entre com o Token JWT",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
					new OpenApiSecurityScheme{
						Reference = new OpenApiReference{
							Id = "Bearer",
							Type = ReferenceType.SecurityScheme
						}
					}, new List<string>()
				}
				});
			});
		}


	}
}