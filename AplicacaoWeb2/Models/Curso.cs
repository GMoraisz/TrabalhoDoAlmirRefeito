using System;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoWeb2.Models
{
    public class Curso
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do curso é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do curso deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "A data de início deve ser uma data válida.")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de término é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "A data de término deve ser uma data válida.")]
        public DateTime DataFim { get; set; }
    }
}
