using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vendedor.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Vendedor.DAL
{
    public class VendedorContext : DbContext
    {
        public VendedorContext() : base("VendedorContext")
        {
        }

        public DbSet<Estado> Estados { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<DadosPessoaFisica> PessoaFisica { get; set; }

        public DbSet<Endereco> Endereco { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}