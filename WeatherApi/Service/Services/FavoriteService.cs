using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Infrastructure.Clients;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Service.Services
{
	public class FavoriteService : IFavoriteService
	{
		private readonly IOpenWeatherMapClient _client;
		private readonly IFavoriteWeatherRepository _repository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public FavoriteService(IOpenWeatherMapClient client, IFavoriteWeatherRepository repository, IHttpContextAccessor httpContextAccessor)
		{
			_client = client;
			_repository = repository;
			_httpContextAccessor = httpContextAccessor;
		}
		public Task<bool> DeleteAsync(string id)
		{
			var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Name)?.Value;
			return _repository.DeleteAsync(id, userId!);
		}

		public async Task<IEnumerable<FavoriteWeatherModel>> GetAsync()
		{
			var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Name)?.Value;
			return await _repository.GetAsync(userId!);
		}

		public async Task<FavoriteWeatherModel> StoreAsync(string location)
		{
			var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Name)?.Value;
			var weather = await _client.GetWeatherAsync(location);
			var favorite = new FavoriteWeatherModel() { Favorite = weather, Id = Guid.NewGuid().ToString(), UserId = userId! };
			await _repository.StoreAsync(favorite);
			return favorite;
		}
	}
}
