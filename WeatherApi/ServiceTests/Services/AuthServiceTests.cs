using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Requests.Auth;
using Infrastructure.Configs;
using Microsoft.Extensions.Options;
using Moq;
using Service.Services;

namespace ServiceTests.Services
{
	public class AuthServiceTests
	{

		private readonly AuthService _sut;
		private readonly MockRepository _mock;
		private readonly Mock<IOptions<JwtConfig>> _jwtConfig;
		private readonly Mock<IUserRepository> _userRepository;

		public AuthServiceTests()
		{
			_mock = new MockRepository(MockBehavior.Loose);
			_userRepository = _mock.Create<IUserRepository>();
			_jwtConfig = _mock.Create<IOptions<JwtConfig>>();

			var jwtConfig = new JwtConfig
			{
				SecretKey = "dGhpcy1pcy1hLXNlY3VyZS1rZXktZm9yLUpXVA==",
				Issuer = "Issuer",
				Audience = "Audience"
			};
			_jwtConfig.Setup(m => m.Value).Returns(jwtConfig);
			_sut = new AuthService(_jwtConfig.Object, _userRepository.Object);
		}

		[Fact]
		public async Task SigninAsync_ShouldFindUser_And_ReturnToken()
		{
			// Arrange

			var request = new SigninRequest()
			{
				Email = "email",
				Password = "password",
			};
			var user = new UserModel()
			{
				Id = "1232",
				Email = "email",
				Name = "name",
				Password = "password",
			};

			_userRepository.Setup(c => c.GetAsync(request.Email, request.Password)).ReturnsAsync(user);
		
			// Act
			var result = await _sut.SigninAsync(request);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(user.Email, request.Email);
			Assert.Equal(user.Password, request.Password);
		}

		[Fact]
		public async Task SigninAsync_ShouldNotFindUser_And_ThrowDomainExecption()
		{
			// Arrange

			var request = new SigninRequest()
			{
				Email = "email",
				Password = "password",
			};
			
			_userRepository.Setup(c => c.GetAsync(request.Email, request.Password)).ReturnsAsync((UserModel?)null);

			// Act
			Task result() => _sut.SigninAsync(request);


			// Assert
			await Assert.ThrowsAsync<DomainException>(result);
		}

		[Fact]
		public async Task SigninAsync_Should_ThrowException()
		{
			// Arrange

			var request = new SigninRequest()
			{
				Email = "email",
				Password = "password",
			};

			_userRepository.Setup(c => c.GetAsync(request.Email, request.Password)).ThrowsAsync(new Exception());

			// Act
			Task result() => _sut.SigninAsync(request);


			// Assert
			await Assert.ThrowsAsync<Exception>(result);
		}

		[Fact]
		public async Task SigninAsync_ShouldCreateUser_And_ReturnToken()
		{
			// Arrange

			var request = new SignupRequest()
			{
				Name = "name",
				Email = "email",
				Password = "password",
			};
			var user = new UserModel(request.Name, request.Email, request.Password);

			_userRepository.Setup(c => c.StoreAsync(user));

			// Act
			var result = await _sut.SignupAsync(request);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(user.Email, request.Email);
			Assert.Equal(user.Password, request.Password);
		}

		[Fact]
		public async Task SignupAsync_Should_ThrowException()
		{
			// Arrange
			var request = new SignupRequest()
			{
				Name = "name",
				Email = "email",
				Password = "password",
			};

			_userRepository.Setup(c => c.StoreAsync(It.IsAny<UserModel>())).ThrowsAsync(new Exception());

			// Act
			Task result() => _sut.SignupAsync(request);


			// Assert
			await Assert.ThrowsAsync<Exception>(result);
		}
	}
}