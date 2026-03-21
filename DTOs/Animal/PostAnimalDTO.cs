using System.ComponentModel.DataAnnotations;

namespace AdocaoPetApi.DTOs.Animal
{
    public class PostAnimalDTO
    {
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "A categoria do animal é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma categoria válida.")]
        public int IdCategoriaAnimal { get; set; }

        [Required(ErrorMessage = "A raça deve ser informada.")]
        [StringLength(50, ErrorMessage = "A raça deve ter no máximo 50 caracteres.")]
        public string Raca { get; set; } = string.Empty;

        [Range(0, 30, ErrorMessage = "A idade deve estar entre 0 e 30 anos.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O sexo é obrigatório.")]
        [RegularExpression("^(Macho|Fêmea)$", ErrorMessage = "O sexo deve ser 'Macho' ou 'Fêmea'.")]
        public string Sexo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Uma breve descrição é necessária.")]
        [MinLength(10, ErrorMessage = "A descrição deve ter pelo menos 10 caracteres.")]
        [MaxLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O porte do animal é obrigatório (Pequeno, Médio ou Grande).")]
        public string Porte { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "O campo saúde deve ter no máximo 200 caracteres.")]
        public string Saude { get; set; } = string.Empty;

        [MaxLength(1000, ErrorMessage = "A história do animal não pode exceder 1000 caracteres.")]
        public string Historia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O status atual é obrigatório (Ex: Disponível, Adotado).")]
        public string Status { get; set; } = string.Empty;

        [Url(ErrorMessage = "A URL da foto não é válida.")]
        public string FotoUrl { get; set; } = string.Empty;
    }
}