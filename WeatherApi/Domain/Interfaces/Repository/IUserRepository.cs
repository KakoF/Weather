using Domain.Models;

namespace Domain.Interfaces.Repository
{
	public interface IUserRepository
	{

		Task StoreAsync(UserModel model);

		Task<UserModel?> GetAsync(string email, string password);
	}
}
