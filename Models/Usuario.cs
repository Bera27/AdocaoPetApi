namespace AdocaoPetApi.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public ICollection<Animal> Animais { get; set; } = [];
        public ICollection<Role> Roles { get; set; } = [];
    }
}