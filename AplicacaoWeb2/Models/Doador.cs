using System.ComponentModel.DataAnnotations;

namespace AplicacaoWeb2.Models
{
    public class Doador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do doador é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O CPF ou CNPJ é obrigatório.")]
        [RegularExpression(@"^\d{11}|\d{14}$", ErrorMessage = "O CPF deve ter 11 dígitos ou o CNPJ deve ter 14 dígitos.")]
        public string CpfCnpj { get; set; }
    }
}
