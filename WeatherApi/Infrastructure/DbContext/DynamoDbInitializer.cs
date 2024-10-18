using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;

namespace Infrastructure.DbContext
{
	public class DynamoDbInitializer
	{
		private readonly IAmazonDynamoDB _client;

		public DynamoDbInitializer(IAmazonDynamoDB client)
		{
			_client = client;
		}

		public async Task InitializeTablesAsync()
		{
			var tableResponse = await _client.ListTablesAsync();
			if (!tableResponse.TableNames.Contains("User"))
				await CreateTableUser();
			if (!tableResponse.TableNames.Contains("Favorite"))
				await CreateTableFavorite();
		}

		private async Task CreateTableUser()
		{
			var createTableRequest = new CreateTableRequest
			{
				TableName = "User",
				KeySchema = new List<KeySchemaElement>
				{
					new KeySchemaElement
					{
						AttributeName = "Id",
						KeyType = KeyType.HASH
					}
				},
				AttributeDefinitions = new List<AttributeDefinition>
				{
					new AttributeDefinition
					{
						AttributeName = "Id",
						AttributeType = ScalarAttributeType.S
					},
				},
				ProvisionedThroughput = new ProvisionedThroughput
				{
					ReadCapacityUnits = 5,
					WriteCapacityUnits = 5
				}
			};

			await _client.CreateTableAsync(createTableRequest);
		}

		private async Task CreateTableFavorite()
		{
			var createTableRequest = new CreateTableRequest
			{
				TableName = "Favorite",
				KeySchema = new List<KeySchemaElement>
				{
					new KeySchemaElement
					{
						AttributeName = "Id",
						KeyType = KeyType.HASH
					},
					new KeySchemaElement
					{
						AttributeName = "UserId",
						KeyType = KeyType.RANGE
					}
				},
				AttributeDefinitions = new List<AttributeDefinition>
				{
					new AttributeDefinition
					{
						AttributeName = "Id",
						AttributeType = ScalarAttributeType.S
					},
					new AttributeDefinition
					{
						AttributeName = "UserId",
						AttributeType = ScalarAttributeType.S
					},
				},
				ProvisionedThroughput = new ProvisionedThroughput
				{
					ReadCapacityUnits = 5,
					WriteCapacityUnits = 5
				}
			};

			await _client.CreateTableAsync(createTableRequest);
		}
	}
}