using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvocaciaPPFinal.Models
{
    public class Livro
    {
        [Key]
        public int Id_Livro { get; set; }
        [Display(Name = "Nome")]
        public string Nome_Livro { get; set; }
        [Display(Name = "Gênero")]
        public string Genero_Livro { get; set; }
        [Display(Name = "Autor")]
        public string Autor_Livro { get; set; }
        [Display(Name = "Editora")]
        public string Editora_Livro { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ano de Publicação")]
        public DateTime Ano_Publicacao { get; set; }
        [Display(Name = "Status")]
        public string Status_Livro { get; set; } // disponivel ou emprestado.
    }
}
