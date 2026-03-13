namespace AdocaoPetApi.Models
{
    public class Animal
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Especie { get; set; } = string.Empty;
        public string Raca { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Porte { get; set; } = string.Empty;
        public string Saude { get; set; } = string.Empty;
        public string Historia { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;
        public string FotoUrl { get; set; } = string.Empty;

        public Usuario Usuario { get; set; } = default!;
        public Guid UsuarioId { get; set; }
    }
}