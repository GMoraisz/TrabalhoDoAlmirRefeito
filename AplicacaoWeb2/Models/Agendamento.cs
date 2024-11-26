using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoWeb2.Models
{
    public class Agendamento
    {
        [Key] // Define explicitamente como chave primária
        public int IdAgendamento { get; set; }

        public string Descricao { get; set; }

        // Relacionamento com Visitas
        public ICollection<Visita> Visitas { get; set; }
    }
}
