using System;
using Microsoft.EntityFrameworkCore;


using Domaine.MyEntities;
using DataAccess.ModelBuilderConfigs;

namespace DataAccess
{
    public class MyApplicationDbContext : DbContext
    {
        public MyApplicationDbContext(DbContextOptions<MyApplicationDbContext> options) : base(options)
        {
            Console.WriteLine($"\n\n - Instanciation de MyApplicationDbContext -\n\n");
        }


        //--------------------------------------
        
        public DbSet<TirageLoto> TiragesLoto { get; set; }
        public DbSet<Numero> Numeros { get; set; }

        public DbSet<NumeroTirageLoto> NumerosTiragesLoto { get; set; }




        //---------- Utilisation de la Fluent API, permet de paramétrer sans passer par des annotations, et donc sans toucher aux classes du Domaine ------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration( new TirageLotoConfig() );
            modelBuilder.ApplyConfiguration(new NumeroConfig());

            modelBuilder.ApplyConfiguration(new NumeroTirageLotoConfig());
        }

    }
}
