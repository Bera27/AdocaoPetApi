using System.ComponentModel.DataAnnotations;

namespace AdocaoPetApi.DTOs.Usuario
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "O Email é inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Senha é obrigatório")]
        public string Senha { get; set; } = string.Empty;
    }
}