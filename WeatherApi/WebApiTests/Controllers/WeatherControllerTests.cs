using Domain.Exceptions;
using Domain.Interfaces.Services;
using Domain.Models;
using Moq;
using WebApi.Controllers;

namespace WebApiTests
{
	public class WeatherControllerTests
	{
		private readonly WeatherController _sut;
		private readonly MockRepository _mock;
		private readonly Mock<IWeatherService> _weatherServiceMock;

		public WeatherControllerTests()
		{
			_mock = new MockRepository(MockBehavior.Loose);
			_weatherServiceMock = _mock.Create<IWeatherService>();
			_sut = new WeatherController(_weatherServiceMock.Object);
		}

		[Fact]
		public async Task GetAsync_ShouldReturn_WeatherLocation()
		{
			// Arrange
			var cancelationToken = new CancellationToken();
			WeatherModel weather = new WeatherModel()
			{
				Id = 1,
				Name = "Brasil",

			};

			_weatherServiceMock.Setup(c => c.GetAsync("Brasil")).ReturnsAsync(weather);

			// Act
			var result = await _sut.GetAsync("Brasil");

			// Assert
			Assert.NotNull(result);
		}


		[Fact]
		public async Task GetAsync_Should_ThrowsException()
		{
			// Arrange
			WeatherModel weather = new WeatherModel()
			{
				Id = 1,
				Name = "Brasil",

			};

			_weatherServiceMock.Setup(c => c.GetAsync("Brasil")).ThrowsAsync(new DomainException("Error message", 400));

			// Act
			Task result() => _sut.GetAsync("Brasil");

			// Assert
			await Assert.ThrowsAsync<DomainException>(result);
		}
	}
}
