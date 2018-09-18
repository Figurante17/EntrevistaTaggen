using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RestauranteTaggen.Models;

namespace RestauranteTaggen.Context
{
    public class ContextRestaurante : DbContext
    {
        public DbSet<Restaurante> restaurantes { get; set; }
        public DbSet<Pratos> Pratos { get; set; }


         protected override void OnModelCreating(DbModelBuilder modelBuilder)
         {
            /*   Database.SetInitializer<ContextRestaurante>(null);
               //modelBuilder.Entity<Pratos>().HasOptional(o => o.Restaurante).WithMany().Map(m => m.MapKey("codigo"));
               base.OnModelCreating(modelBuilder);*/
        }
    }
}