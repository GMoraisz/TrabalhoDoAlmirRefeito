using System;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoWeb2.Models
{
    public class Visita
    {
        [Key] // Define explicitamente como chave primária
        public int IdVisita { get; set; }

        // Relacionamento com Agendamento
        public int IdAgendamento { get; set; }
        public Agendamento Agendamento { get; set; }

        public DateTime DataVisita { get; set; }
    }
}
