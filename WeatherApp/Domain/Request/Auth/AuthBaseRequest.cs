using System.ComponentModel.DataAnnotations;

namespace Domain.Request.Auth
{
    public class AuthBaseRequest
    {
        [Required(ErrorMessage = "E-mail é campo obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [StringLength(100, ErrorMessage = "E-mail deve ter no máximo {1} caracteres.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password é campo obrigatório.")]
        public string Password { get; set; } = null!;
    }
}
