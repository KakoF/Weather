using System.ComponentModel.DataAnnotations;
namespace Domain.Requests.Auth
{
	public class SignupRequest : AuthBaseRequest
	{
		[Required(ErrorMessage = "Nome é campo obrigatório.")]
		[StringLength(100, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
		public string Name { get; set; } = null!;
	}
}
