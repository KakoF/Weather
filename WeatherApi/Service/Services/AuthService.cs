using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Requests.Auth;
using Infrastructure.Configs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Service.Services
{
	public class AuthService : IAuthService
	{
		private readonly JwtConfig _signinConfigurations;
		private readonly IUserRepository _repository;
		public AuthService(IOptions<JwtConfig> jwtConfig, IUserRepository repository)
		{
			_signinConfigurations = jwtConfig.Value;
			_repository = repository;
		}
		public async Task<string?> SigninAsync(SigninRequest request)
		{
			var user = await _repository.GetAsync(request.Email, request.Password);
			if (user == null)
				return default;

			var token = GenerateIdentityToken(user);
			return token;
		}

		public async Task<string> SignupAsync(SignupRequest request)
		{
			var user = new UserModel(request.Name, request.Email, request.Password);
			await _repository.StoreAsync(user);
			var token = GenerateIdentityToken(user);
			return token;
		}

		private string GenerateIdentityToken(UserModel user)
		{
			var key = Encoding.ASCII.GetBytes(_signinConfigurations.SecretKey);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(
				  new GenericIdentity(user.Id.ToString()),
				  new[]{
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						new Claim(JwtRegisteredClaimNames.Name, user.Name),
						new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
				  }
				),
				Expires = DateTime.UtcNow.AddMinutes(_signinConfigurations.ExpiryMinutes),
				Issuer = _signinConfigurations.Issuer,
				Audience = _signinConfigurations.Audience,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}