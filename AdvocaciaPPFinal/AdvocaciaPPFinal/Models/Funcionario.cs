using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvocaciaPPFinal.Models
{
    public class Funcionario
    {
        [Key]
        public int Id_Funcionario { get; set; }
        [Display(Name = "Nome")]
        public string Nome_Funcionario { get; set; }
        [Display(Name = "CPF")]
        public string CPF_Funcionario { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento_Funcionario { get; set; }
        [Display(Name = "Cargo")]
        public string Cargo_Funcionario { get; set; }
        [Display(Name = "CEP")]
        public long CEP_Funcionario { get; set; }
        [Display(Name = "Rua")]
        public string Rua_Funcionario { get; set; }
        [Display(Name = "Numero")]
        public int Numero_Funcionario { get; set; }
        [Display(Name = "Cidade")]
        public string Cidade_Funcionario { get; set; }
        [Display(Name = "Bairro")]
        public string Bairro_Funcionario { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro_Funcionario { get; set; }
    }
}
