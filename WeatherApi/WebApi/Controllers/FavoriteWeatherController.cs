using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[ApiController]
	[Authorize]
	[Route("[controller]")]
	public class FavoriteWeatherController : ControllerBase
	{

		private readonly IFavoriteService _service;

		public FavoriteWeatherController(IFavoriteService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IEnumerable<FavoriteWeatherModel>> GetAsync()
		{

			return await _service.GetAsync();
		}
		[HttpPost]
		public async Task<FavoriteWeatherModel> StoreAsync([FromBody] string location)
		{
			return await _service.StoreAsync(location);
		}
		[HttpDelete]
		[Route("{id}")]
		public async Task<bool> DeleteAsync(string id)
		{
			return await _service.DeleteAsync(id);
		}
	}
}
