using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestauranteTaggen.Models
{
    public class PratosViewModel
    {
        public String NomePrato { get; set; }
        public decimal Preco { get; set; }
        public String NomeRestaurante { get; set; }      
        public int CodigoPratos { get; set; }
        public int IdRestaurante { get; set; }
    }
}