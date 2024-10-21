using Domain.Models;

namespace Domain.Interfaces.Services
{
	public interface IFavoriteService
	{
		Task<FavoriteWeatherModel> StoreAsync(string location);
		Task<bool> DeleteAsync(string id);
		Task<IEnumerable<FavoriteWeatherModel>> GetAsync();
	}
}
