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
            mesTests.Test_Passagers_AvecLeurVol_via_Select_et_Where();
            mesTests.Test_Passagers_AvecLeurVol_via_Join();

            //- Liste des Vols avec leurs Passagers
            mesTests.Test_Vols_AvecLeursPassagers_via_Select_et_Where();
            mesTests.Test_Vols_AvecLeursPassagers_via_GroupJoin();

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
