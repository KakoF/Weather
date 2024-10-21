using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Requests.Auth;
using Infrastructure.Clients;
using Infrastructure.Configs;
using Microsoft.Extensions.Options;
using Moq;
using Service.Services;

namespace ServiceTests.Services
{
	public class WeatherServiceTests
	{

		private readonly WeatherService _sut;
		private readonly MockRepository _mock;
		private readonly Mock<IOpenWeatherMapClient> _openWeatherMapClient;

		public WeatherServiceTests()
		{
			_mock = new MockRepository(MockBehavior.Loose);
			_openWeatherMapClient = _mock.Create<IOpenWeatherMapClient>();
			_sut = new WeatherService(_openWeatherMapClient.Object);
		}

		[Fact]
		public async Task GetAsync_ShouldFindLocation_Return()
		{
			// Arrange
			string location = "Brasil";

			_openWeatherMapClient.Setup(c => c.GetWeatherAsync(location)).ReturnsAsync(It.IsAny<WeatherModel>());

			// Act
			var result = await _sut.GetAsync(location);

			// Assert
			_openWeatherMapClient.Verify(c => c.GetWeatherAsync(location), Times.Once);
		}

		[Fact]
		public async Task GetAsync_ShouldNotFindLocation_And_ThrowDomainExecption()
		{
			// Arrange
			string location = "Brasil";


			_openWeatherMapClient.Setup(c => c.GetWeatherAsync(location)).ThrowsAsync(new DomainException());

			// Act
			Task result() => _sut.GetAsync(location);


			// Assert
			await Assert.ThrowsAsync<DomainException>(result);
		}
	}
}
