using System;
using Microsoft.Extensions.DependencyInjection; //IServiceCollection, ServiceCollection

using Data;

namespace ConsolePrj
{
    static class Program
    {
        private static IServiceProvider oServicesProvider;

        static void Main(string[] args)
        {
            ConfigInjections();

            MesTests mesTests = oServicesProvider.GetService<MesTests>();

            //- Liste des Passagers et de leur Vol
            mesTests.TestJoin_Passagers_AvecLeurVol();

            //- Liste des Vols avec leurs Passagers
            mesTests.TestJoin_Vols_AvecLeursPassagers_Avec_GroupBy_Select();
            mesTests.TestGroupJoin_Vols_AvecLeursPassagers();

            Console.WriteLine("\n\nOk"); Console.ReadKey();
        }

        private static void ConfigInjections()
        {
            IServiceCollection oServicesCollection = new ServiceCollection();

            oServicesCollection
                .AddSingleton<MesTests>() //Equivaut à AddSingleton<MesTests, MesTests>
                .AddSingleton(
                    (IServiceProvider poServiceProvider) =>
                        DataSourceFactory.GetInstance().GetSingleton() //IDataSource
                );

            oServicesProvider = oServicesCollection.BuildServiceProvider();
        }
    }
}
