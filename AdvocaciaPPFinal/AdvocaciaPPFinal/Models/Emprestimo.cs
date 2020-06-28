using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvocaciaPPFinal.Models
{
    public class Emprestimo
    {
        [Key]
        public int Id_Emprestimo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Valor de Empréstimo")]
        public decimal Valor_Emprestimo { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data de Empréstimo")]
        public DateTime Data_Emprestimo { get; set; }
    }
}
