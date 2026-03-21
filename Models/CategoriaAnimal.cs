namespace AdocaoPetApi.Models
{
    public class CategoriaAnimal
    {
        public int Id { get; set; }
        public string NomeCategoria { get; set; } = string.Empty;

        public ICollection<Animal> AnimaisCategorias { get; set; } = [];
    }
}