using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdocaoPetApi.DTOs.Animal
{
    public class GetAnimalDTO
    {
        // Dados do Doador
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        // Dados do Pet
        public Guid Id { get; set; }
        public string Especie { get; set; } = string.Empty;
        public string Raca { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Porte { get; set; } = string.Empty;
        public string Saude { get; set; } = string.Empty;
        public string Historia { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public string Status { get; set; } = string.Empty;
        public string FotoUrl { get; set; } = string.Empty;
    }
}