using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Requests.Auth;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApiTests.Controllers
{
    public class AuthControllerTests
    {

		private readonly AuthController _sut;
		private readonly MockRepository _mock;
		private readonly Mock<IAuthService> _authService;

		public AuthControllerTests()
		{
			_mock = new MockRepository(MockBehavior.Loose);
			_authService = _mock.Create<IAuthService>();
			_sut = new AuthController(_authService.Object);
		}

		[Fact]
		public async Task SignupAsync_ShouldReturn_Token()
		{
			// Arrange
			var request = new SignupRequest()
			{
				Email = "email",
				Name = "name",
				Password = "password",
			};
			var token = "token";

			_authService.Setup(c => c.SignupAsync(request)).ReturnsAsync(token);

			// Act
			var result = await _sut.SignupAsync(request);

			// Assert
			Assert.NotNull(result);
		}

		[Fact]
		public async Task SignupAsync_Should_ThrowsException()
		{
			// Arrange
			var request = new SignupRequest()
			{
				Email = "email",
				Name = "name",
				Password = "password",
			};
			var token = "token";

			_authService.Setup(c => c.SignupAsync(request)).ThrowsAsync(new Exception());

			// Act
			Task result() => _sut.SignupAsync(request);

			// Assert
			await Assert.ThrowsAsync<Exception>(result);

		}


		[Fact]
		public async Task SigninAsync_ShouldReturn_Token()
		{
			// Arrange
			var request = new SigninRequest()
			{
				Email = "email",
				Password = "password",
			};
			var token = "token";

			_authService.Setup(c => c.SigninAsync(request)).ReturnsAsync(token);

			// Act
			var result = await _sut.SigninAsync(request);

			// Assert
			Assert.NotNull(result);
		}

		[Fact]
		public async Task SigninAsync_Should_ThrowsException()
		{
			// Arrange
			var request = new SigninRequest()
			{
				Email = "email",
				Password = "password",
			};
			var token = "token";

			_authService.Setup(c => c.SigninAsync(request)).ThrowsAsync(new Exception());

			// Act
			Task result() => _sut.SigninAsync(request);

			// Assert
			await Assert.ThrowsAsync<Exception>(result);

		}

	}
}
