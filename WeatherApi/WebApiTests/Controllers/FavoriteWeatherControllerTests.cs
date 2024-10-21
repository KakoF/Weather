using Domain.Exceptions;
using Domain.Interfaces.Services;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApiTests
{
	public class FavoriteWeatherControllerTests
	{
		private readonly FavoriteWeatherController _sut;
		private readonly MockRepository _mock;
		private readonly Mock<IFavoriteService> _favoriteService;

		public FavoriteWeatherControllerTests()
		{
			_mock = new MockRepository(MockBehavior.Loose);
			_favoriteService = _mock.Create<IFavoriteService>();
			_sut = new FavoriteWeatherController(_favoriteService.Object);
		}

		[Fact]
		public async Task GetAsync_ShouldReturn_FavoritesWeathersLocation()
		{
			//Assert
			var wathers = new List<FavoriteWeatherModel>
			{
				new FavoriteWeatherModel()
				{
					Id = "1",
					UserId = It.IsAny<string>(),
					Favorite = new WeatherModel()
				}
			};

			_favoriteService.Setup(c => c.GetAsync()).ReturnsAsync(wathers);

			// Act
			var result = await _sut.GetAsync();

			// Assert
			Assert.NotNull(result);
		}

		[Fact]
		public async Task GetAsync_Should_ThrowsException()
		{
			// Arrange
			var wathers = new List<FavoriteWeatherModel>
			{
				new FavoriteWeatherModel()
				{
					Id = "1",
					UserId = It.IsAny<string>(),
					Favorite = new WeatherModel()
				}
			};

			_favoriteService.Setup(c => c.GetAsync()).ThrowsAsync(new Exception());

			// Act
			Task result() => _sut.GetAsync();

			// Assert
			await Assert.ThrowsAsync<Exception>(result);
		}


		[Fact]
		public async Task StoreAsync_ShouldReturn_FavoritesWeathers_Save()
		{
			// Arrange
			string location = "Brasil";
			var wather = new FavoriteWeatherModel()
			{
				Id = "1",
				UserId = It.IsAny<string>(),
				Favorite = new WeatherModel()
			};

			_favoriteService.Setup(c => c.StoreAsync(location)).ReturnsAsync(wather);

			// Act
			var result = await _sut.StoreAsync(location);

			// Assert
			Assert.NotNull(result);
		}

		[Fact]
		public async Task StoreAsync_Should_ThrowsException()
		{
			// Arrange
			string location = "Brasil";
			var wather = new FavoriteWeatherModel()
			{
				Id = "1",
				UserId = It.IsAny<string>(),
				Favorite = new WeatherModel()
			};

			_favoriteService.Setup(c => c.StoreAsync(location)).ThrowsAsync(new Exception());


			// Act
			Task result() => _sut.StoreAsync(location);

			// Assert
			await Assert.ThrowsAsync<Exception>(result);
		}


		[Fact]
		public async Task DeleteAsync_ShouldReturn_True()
		{
			// Arrange
			string id = "123";

			_favoriteService.Setup(c => c.DeleteAsync(id)).ReturnsAsync(true);

			// Act
			var result = await _sut.DeleteAsync(id);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public async Task DeleteAsync_Should_ThrowsException()
		{
			// Arrange
			string id = "123";

			_favoriteService.Setup(c => c.DeleteAsync(id)).ThrowsAsync(new Exception());

			// Act
			Task result() => _sut.DeleteAsync(id);

			// Assert
			await Assert.ThrowsAsync<Exception>(result);
		}
	}
}