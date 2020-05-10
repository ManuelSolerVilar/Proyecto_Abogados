using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ProyectoAbogadosV2.Models
{
    public class SoporteContexto : DbContext
    {

        public SoporteContexto() : base("name=SoporteContexto")
        { }

        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Actuacion> Actuaciones { get; set; }
        public DbSet<Expediente> Expedientes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Jurisdiccion> Jurisdicciones { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }


    }
}