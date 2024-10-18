using Domain.Models;

namespace Domain.Interfaces.Repository
{
	public interface IFavoriteWeatherRepository
	{
		Task StoreAsync(FavoriteWeatherModel model);
		Task<IEnumerable<FavoriteWeatherModel>> GetAsync(string id);
		Task<bool> DeleteAsync(string id, string userId);
	}
}
