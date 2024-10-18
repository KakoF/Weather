using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.Configuration;
using Amazon;

namespace Infrastructure.DbContext
{
	public class DynamoDbContext
	{
		public DynamoDBContext Context { get; }

		public DynamoDbContext(IConfiguration configuration)
		{
			var client = new AmazonDynamoDBClient(
					configuration["AWS:AccessKey"],
					configuration["AWS:SecretKey"],
					RegionEndpoint.GetBySystemName(configuration["AWS:Region"])
					);
			Context = new DynamoDBContext(client);
		}
	}
}
