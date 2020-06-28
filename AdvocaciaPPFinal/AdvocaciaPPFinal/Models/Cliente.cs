using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvocaciaPPFinal.Models
{
    public class Cliente
    {
        [Key]
        public int Id_Cliente { get; set; }
        [Display(Name = "Nome")]
        public string Nome_Cliente { get; set; }
        [Display(Name = "CPF")]
        public string CPF_Cliente { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento_Cliente { get; set; }
        [Display(Name = "Telefone")]
        public long Telefone_Cliente { get; set; }
        [Display(Name = "E-mail")]
        public string Email_Cliente { get; set; }
        public long CEP { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro_Cliente { get; set; }

    }
}
