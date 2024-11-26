using System.ComponentModel.DataAnnotations;

namespace AplicacaoWeb2.Models
{
    public class Responsavel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do responsável é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [RegularExpression(@"^\(\d{2}\)\s\d{4,5}-\d{4}$", ErrorMessage = "O telefone deve estar no formato (XX) XXXXX-XXXX.")]
        public string Telefone { get; set; }

        [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres.")]
        public string Endereco { get; set; }
    }
}
