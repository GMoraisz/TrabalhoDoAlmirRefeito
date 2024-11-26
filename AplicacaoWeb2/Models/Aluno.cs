using System;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoWeb2.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "A data de nascimento deve ser uma data válida.")]
        public DateTime Nascimento { get; set; }

        [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres.")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 dígitos.")]
        public string CPF { get; set; }
    }
}
