
using Domain.Requests.Auth;

namespace Domain.Interfaces.Services
{
	public interface IAuthService
	{
		Task<string> SignupAsync(SignupRequest request);
		Task<string?> SigninAsync(SigninRequest request);

	}
}
