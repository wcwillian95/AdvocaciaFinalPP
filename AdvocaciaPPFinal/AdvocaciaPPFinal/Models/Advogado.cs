using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvocaciaPPFinal.Models
{
    public class Advogado
    {
        [Key]
        public int Id_Advogado { get; set; }
        [Display(Name = "Nome Completo")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Nome inválido")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho inválido")]
        public string Nome_Advogado { get; set; }
        [Display(Name = "CPF")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "CPF inválido")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Tamanho do CPF inválido")]
        public string CPF_Advogado { get; set; }
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime Data_de_nascimento { get; set; }
        [Display(Name = "Inscrição")]
        public string Inscricao_Advogado { get; set; }
        [Display(Name = "Instituição")]
        public string Instituicao_Advogado { get; set; }
        [Display(Name = "Especialização")]
        public string Especializacao_Advogado { get; set; }
        [Display(Name = "Telefone")]
        public long Telefone_Advogado { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email_Advogado { get; set; }
        [Display(Name = "Numero de Casos")]
        public int Numero_casos { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="Data de Admissão")]
        public DateTime DataAdmissao_Advogado { get; set; }
    }
}
