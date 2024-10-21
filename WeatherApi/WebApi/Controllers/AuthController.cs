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
		public async Task<string> SignupAsync([FromBody] SignupRequest request)
		{
			return await _service.SignupAsync(request);
		}

		[HttpPost]
		[Route("Signin")]
		public async Task<string?> SigninAsync([FromBody] SigninRequest request)
		{
			return await _service.SigninAsync(request);
		}
	}
}
