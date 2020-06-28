using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvocaciaPPFinal.Models
{
    public class Estagiario
    {
        [Key]
        public int Id_Estagiario { get; set; }
        [Display(Name = "Nome")]
        public string Nome_Estagiario { get; set; }
        [Display(Name = "CPF")]
        public string CPF_Estagiario { get; set; }
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento_Estagiario{ get; set; }
        [Display(Name = "Telefone")]
        public long Telefone_Estagiario { get; set; }
        [Display(Name = "E-mail")]
        public string Email_Estagiario { get; set; }
        [Display(Name = "Função")]
        public string Funcao { get; set; } //setor que ele trabalha
        [DataType(DataType.Date)]
        public DateTime DataCadastro_Estagiario { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fim de Contrato")]
        public DateTime Data_fim_contrato { get; set; }
        public string Comentario { get; set; } // No trello o josé pediu para adicionar isso josé pediu para adicionar isso 
        //fazer este campo opcional para caso seja menor de idade 
        [Display(Name = "Nome do Responsável")]
        public string Nome_Resonsavel { get; set; }
        [Display(Name = "Telefone do Responsável")]
        public long Telefone_Responsalvel { get; set; }
    }
}
