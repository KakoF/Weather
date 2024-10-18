using Domain.Interfaces.Services;
using Domain.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _service;
		public AuthController(IAuthService service)
		{
			_service = service;
		}

		[HttpPost]
		[Route("Signup")]
		public async Task<string> Signup([FromBody] SignupRequest request)
		{
			return await _service.SignupAsync(request);
		}

		[HttpPost]
		[Route("Signin")]
		public async Task<string?> GetAsync([FromBody] SigninRequest request)
		{
			return await _service.SigninAsync(request);
		}
	}
}
