using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using Service.Services;
using System.Security.Claims;

namespace ServiceTests.Services
{
	public class FavoriteServiceTests
	{
		private readonly FavoriteService _sut;
		private readonly MockRepository _mock;
		private readonly Mock<IWeatherService> _weatherService;
		private readonly Mock<IFavoriteWeatherRepository> _repository;
		private readonly Mock<IHttpContextAccessor> _httpContextAccessor;

		public FavoriteServiceTests()
		{
			_mock = new MockRepository(MockBehavior.Loose);
			_weatherService = _mock.Create<IWeatherService>();
			_repository = _mock.Create<IFavoriteWeatherRepository>();
			_httpContextAccessor = _mock.Create<IHttpContextAccessor>();

			var mockHttpContext = new Mock<HttpContext>();
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, "12345")
			};
			var identity = new ClaimsIdentity(claims, "Mock");
			var claimsPrincipal = new ClaimsPrincipal(identity);
			_httpContextAccessor.Setup(m => m.HttpContext).Returns(mockHttpContext.Object);
			mockHttpContext.Setup(m => m.User).Returns(claimsPrincipal);
			_sut = new FavoriteService(_weatherService.Object, _repository.Object, _httpContextAccessor.Object);
		}

		[Fact]
		public async Task DeleteAsync_ShouldDeleteFavorite_And_ReturnTrue()
		{
			// Arrange
			var id = "123";
			var userId = _httpContextAccessor.Object.HttpContext!.User.FindFirst(ClaimTypes.Name)?.Value;


			_repository.Setup(c => c.DeleteAsync(id, userId!)).ReturnsAsync(true);

			// Act
			var result = await _sut.DeleteAsync(id);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public async Task DeleteAsync_Should_ThrowException()
		{
			// Arrange

			var id = "123";
			var userId = _httpContextAccessor.Object.HttpContext!.User.FindFirst(ClaimTypes.Name)?.Value;

			_repository.Setup(c => c.DeleteAsync(id, userId!)).ThrowsAsync(new Exception());


			// Act
			Task result() => _sut.DeleteAsync(id);


			// Assert
			await Assert.ThrowsAsync<Exception>(result);
		}


		[Fact]
		public async Task GetAsync_ShouldGet_FavoriteList()
		{
			// Arrange
			var userId = _httpContextAccessor.Object.HttpContext!.User.FindFirst(ClaimTypes.Name)?.Value;


			_repository.Setup(c => c.GetAsync(userId!)).ReturnsAsync(It.IsAny<IEnumerable<FavoriteWeatherModel>>());

			// Act
			var result = await _sut.GetAsync();

			// Assert
			_repository.Verify(c => c.GetAsync(userId!), Times.Once);
		}

		[Fact]
		public async Task GetAsync_Should_ThrowException()
		{
			// Arrange
			var userId = _httpContextAccessor.Object.HttpContext!.User.FindFirst(ClaimTypes.Name)?.Value;

			_repository.Setup(c => c.GetAsync(userId!)).ThrowsAsync(new Exception());

			// Act
			Task result() => _sut.GetAsync();


			// Assert
			await Assert.ThrowsAsync<Exception>(result);
		}


		[Fact]
		public async Task StoreAsync_Should_SaveFavorite()
		{
			// Arrange
			string location = "Brasil";
			var userId = _httpContextAccessor.Object.HttpContext!.User.FindFirst(ClaimTypes.Name)?.Value;

			_weatherService.Setup(c => c.GetAsync(location)).ReturnsAsync(It.IsAny<WeatherModel>());
			_repository.Setup(c => c.StoreAsync(It.IsAny<FavoriteWeatherModel>()));

			// Act
			var result = await _sut.StoreAsync(location);

			// Assert
			_repository.Verify(c => c.StoreAsync(It.IsAny<FavoriteWeatherModel>()), Times.Once);
			_weatherService.Verify(c => c.GetAsync(location), Times.Once);
		}

		[Fact]
		public async Task StoreAsync_Should_ThrowDomainException()
		{
			// Arrange
			string location = "Brasil";
			var userId = _httpContextAccessor.Object.HttpContext!.User.FindFirst(ClaimTypes.Name)?.Value;

			_weatherService.Setup(c => c.GetAsync(location)).ThrowsAsync(new DomainException("Localização nao encontrada"));

			_repository.Setup(c => c.StoreAsync(It.IsAny<FavoriteWeatherModel>()));

			// Act
			Task result() => _sut.StoreAsync(location);


			// Assert
			await Assert.ThrowsAsync<DomainException>(result);
			_repository.Verify(c => c.StoreAsync(It.IsAny<FavoriteWeatherModel>()), Times.Never);
			_weatherService.Verify(c => c.GetAsync(location), Times.Once);
		}


		[Fact]
		public async Task StoreAsync_Should_ThrowException()
		{
			// Arrange
			string location = "Brasil";
			var userId = _httpContextAccessor.Object.HttpContext!.User.FindFirst(ClaimTypes.Name)?.Value;
			
			_weatherService.Setup(c => c.GetAsync(location)).ReturnsAsync(It.IsAny<WeatherModel>());
			_repository.Setup(c => c.StoreAsync(It.IsAny<FavoriteWeatherModel>())).ThrowsAsync(new Exception());

			// Act
			Task result() => _sut.StoreAsync(location);


			// Assert
			await Assert.ThrowsAsync<Exception>(result);
			_repository.Verify(c => c.StoreAsync(It.IsAny<FavoriteWeatherModel>()), Times.Once);
			_weatherService.Verify(c => c.GetAsync(location), Times.Once);
		}
	}
}
