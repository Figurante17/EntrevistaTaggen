using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace RestauranteTaggen.Models
{
    public class Pratos
    {
        [Key]
        public int CodigoPratos { get; set; }

        [Required(ErrorMessage = "Nome do prato deve ser preenchido")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Preço do prato é invalido")]
        public decimal Preco { get; set; }

        public int codigoRestaurante { get; set; }


    }
}