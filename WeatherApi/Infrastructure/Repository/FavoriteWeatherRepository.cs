using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.DbContext;

namespace Infrastructure.Repository
{
	public class FavoriteWeatherRepository : IFavoriteWeatherRepository
	{
		private readonly DynamoDbContext _context;

		public FavoriteWeatherRepository(DynamoDbContext context)
		{
			_context = context;
		}

		public async Task StoreAsync(FavoriteWeatherModel model)
		{
			await _context.Context.SaveAsync(model);
		}

		public async Task<IEnumerable<FavoriteWeatherModel>> GetAsync(string userId)
		{
			var conditions = new List<ScanCondition>
			{
				new ScanCondition("UserId", ScanOperator.Equal, userId),
			};

			var response = _context.Context.ScanAsync<FavoriteWeatherModel>(conditions);
			return await response.GetNextSetAsync();
		}

		public async Task<bool> DeleteAsync(string id, string userId)
		{
			var response = _context.Context.DeleteAsync(new FavoriteWeatherModel()
			{
				Id = id,
				UserId = userId
			});
			return await Task.FromResult(true);
		}

	}
}