using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Transverse.Common.DebugTools;  //Issu d'un Nuget perso. mis dans ./../../../../____Common/zzMyLocalPublishedPackages/

using Infra.Common.DataAccess.Interfaces;  //Issu d'un Nuget perso. mis dans ./../../../../____Common/zzMyLocalPublishedPackages/
using Infra.Common.DataAccess;  //Issu d'un Nuget perso. mis dans ./../../../../____Common/zzMyLocalPublishedPackages/

using DataAccess;

namespace ConsolePrj
{
    //Classe nécessaire pour que l'objet DbContext puisse être créé par EF, dans un environnement non ASP.NET (ici envir. Console).
    //Seule la méthode CreateDbContext lui est utile.
    public class MyApplicationDbContextFactory : IDesignTimeDbContextFactory<MyApplicationDbContext> //Du fait que la présente classe implémente cette interface là,
    {                                                                                                // EF saura comment créer son instance de type
                                                                                                     //  MyApplicationDbContext
                                                                                                     // (la méthode CreateDbContext sera alors appelée automatiquement).

        public MyApplicationDbContextFactory() //Constructeur appelé automatiquement par EF pour son besoin...
            : this(false) //Appel perso. vers mon constructeur perso. qui lui prend un param.
        {
        }

        public MyApplicationDbContextFactory(bool appelManuel) //Constructeur perso. !
        {
            var text = (appelManuel) ? "(appel manuel)" : "(appel automatique)";
            //Console.WriteLine($"\n\n - Instanciation de MyApplicationDbContextFactory {text} -\n\n");
        }

        public MyApplicationDbContext CreateDbContext(string[] args) //sera appelée automatiquement par EF en cas de besoin
        {
            return GetDbContext();
        }

        public MyApplicationDbContext GetDbContext() //Méthode perso. !
        {
            IDBServerAccessConfiguration dbServerAccessConfiguration = new DBServerAccessConfiguration()
            {
                DatabaseName = "Essais_EF_Include_ManyToMany"
            };
            //Debug.ShowData(dbServerAccessConfiguration);

            var connectionString = dbServerAccessConfiguration.GetConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<MyApplicationDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new MyApplicationDbContext(optionsBuilder.Options);
        }
    }
}