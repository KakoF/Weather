
using Amazon.DynamoDBv2.DataModel;

namespace Domain.Models
{
	[DynamoDBTable("User")]
	public class UserModel
	{
		[DynamoDBHashKey]
		public string Id { get; set; } = null!;
		[DynamoDBProperty]
		public string Name { get; set; } = null!;
		[DynamoDBProperty]
		public string Email { get; set; } = null!;
		[DynamoDBProperty]
		public string Password { get; set; } = null!;

		public UserModel()
		{

		}

		public UserModel(string id, string name, string email, string password)
		{
			Id = id;
			Name = name;
			Email = email;
			Password = password;
		}

		public UserModel(string name, string email, string password)
		{
			Id = Guid.NewGuid().ToString();
			Name = name;
			Email = email;
			Password = password;
		}

	}
}
