using System;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoWeb2.Models
{
    public class Movimentacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Tipo { get; set; } // Entrada, Saída, Transferência

        public string Origem { get; set; }

        public string Destino { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataMovimentacao { get; set; }

        [Required]
        public decimal Valor { get; set; }

        public string Descricao { get; set; }
    }
}
