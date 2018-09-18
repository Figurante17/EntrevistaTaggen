using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestauranteTaggen.Models
{
    public class Restaurante
    {
        [Key]
        public int codigo { get; set; }
        [Required(ErrorMessage ="Nome do restaurante deve ser preenchido")]
        public String nome { get; set; }

        [ForeignKey("codigoRestaurante")] 
        public ICollection<Pratos> Pratos { get; set; }
    }
}