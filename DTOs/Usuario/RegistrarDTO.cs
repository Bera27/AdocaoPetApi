using System.ComponentModel.DataAnnotations;

namespace AdocaoPetApi.DTOs
{
    public class RegistrarDTO
    {
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Telefone é obrigatório")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "O Email é inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Senha é obrigatório")]
        public string Senha { get; set; } = string.Empty;
    }
}