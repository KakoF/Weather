using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.DbContext;

namespace Infrastructure.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly DynamoDbContext _context;

		public UserRepository(DynamoDbContext context)
		{
			_context = context;
		}

		public async Task StoreAsync(UserModel model)
		{
			await _context.Context.SaveAsync(model);
		}

		public async Task<UserModel?> GetAsync(string email, string password)
		{
			var conditions = new List<ScanCondition>
			{
				new ScanCondition("Email", ScanOperator.Equal, email),
				new ScanCondition("Password", ScanOperator.Equal, password)
			};

			var response = _context.Context.ScanAsync<UserModel>(conditions);
			var results = await response.GetNextSetAsync();
			return results.FirstOrDefault();
		}
	}
}