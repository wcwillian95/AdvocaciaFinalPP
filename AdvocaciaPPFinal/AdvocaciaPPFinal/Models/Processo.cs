using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvocaciaPPFinal.Models
{
    public class Processo
    {
        [Key]
        public int Id_Processo { get; set; }
        [Display(Name = "Advogado Responsável")]
        public string NomeAdvogado_Processo { get; set; }
        [Display(Name = "Cliente")]
        public string NomeCliente_Processo { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Inicio do Processo")]
        public DateTime Data_de_Inicio { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fim de Processo")]
        public DateTime Data_de_termino { get; set; }
        public string Status { get; set; } // vencido ou perdido
    }
}
