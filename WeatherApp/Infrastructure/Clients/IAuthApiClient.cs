using Domain.Models;
using Domain.Request.Auth;
using Refit;

namespace Infrastructure.Clients
{
	public interface IAuthApiClient
    {
		[Post("/Auth/Signup")]
		Task<string> SignupAsync([Body] SignupRequest request);

        [Post("/Auth/Signin")]
        Task<string> SigninAsync([Body] SigninRequest request);

    }
}
