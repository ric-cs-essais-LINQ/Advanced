using System;
using System.Linq;

using Transverse.Common.DebugTools;

using Data.Interfaces;
using Entites;


#pragma warning disable S125 // Sections of code should not be commented out
namespace ConsolePrj
{
    public class MesTests
    {
        private readonly IDataSource dataSource;
        private readonly Chrono chrono;

        public MesTests(IDataSource dataSource)
        {
            this.dataSource = dataSource;
            chrono = new Chrono();
        }

        public void TestJoin_Passagers_AvecLeurVol()
        {
            Console.WriteLine("\n\n\n\n----- TestJoin_Passagers_AvecLeurVol     (  Vol   1<------*   Passager) -----\n");

            // > Rappel : les new {...}, permettent de décrire des objets libres dits de type : anonyme.

            //Debug.ShowData(dataSource.GetPassagers());
            //Debug.ShowData(dataSource.GetVols().Select(vol => new { RefVol=vol.Ref, DateProchainDepart = vol.DateProchainDepart.ToString() })); //Select(), Juste pour le ToString() permettant un formatage correct de la Date lors de l'affichage

            var passagers = dataSource.GetPassagers();
            var vols = dataSource.GetVols();

            chrono.Start();
            var passagersAvecDetailVol = passagers.Join( //Passagers InnerJoin Vols ON passager.Refvol = vol.Ref
                vols,
                passager => passager.RefVol, //Jointure sur passager.RefVol == vol.Ref
                vol => vol.Ref,

                (passager, vol) => //Projection  (Select sur l'enregistrement résultat de la jointure)
                new
                {
                    passager, //<<<Syntaxe équivalant à passager = passager
                    //LeVol = new
                    //{
                        // Ref = vol.Ref,
                        DateProchainDepart = vol.DateProchainDepart.ToString(), //Juste pour le ToString() permettant un formatage correct de la Date lors de l'affichage
                    //}
                }
            );

            Debug.ShowData(passagersAvecDetailVol.ToList());
            chrono.StopAndShowDuration();

            Console.ReadKey();
        }


        public void TestJoin_Vols_AvecLeursPassagers_Avec_GroupBy_Select() //Une bidouille avec Join et GroupBy et Select, pour obtenir la liste des vols et de leurs passagers.
        {
            Console.WriteLine("\n\n\n\n----- TestJoin_Vols_AvecLeursPassagers_Avec_GroupBy_Select    (  Vol   1------>*   Passager)   (assez rapide)  -----\n");

            var passagers = dataSource.GetPassagers();
            var vols = dataSource.GetVols();

            chrono.Start();
            var volsAvecPassagers = vols.Join( //Vols InnerJoin Passagers ON vol.Ref = passager.Refvol
                passagers,
                vol => vol.Ref,                 //Jointure sur vol.Ref == passager.RefVol
                passager => passager.RefVol,

                (vol, passager) => //Projection  (Select sur l'enregistrement résultat de la jointure)
                    new VolAvecPassagers()
                    {
                        RefVol = vol.Ref,
                        DateProchainDepart = vol.DateProchainDepart.ToString(), //Juste pour le ToString() permettant un formatage correct de la Date lors de l'affichage
                        Passagers = passagers.Where(p => p.RefVol == vol.Ref).ToList() //Pas optimal, on est d'accord :)
                    }
                
            )
            .GroupBy(volAvecPassagers => volAvecPassagers.RefVol) //Permet d'avoir un tableau/liste(de VolAvecPassagers) pour chaque volAvecPassagers.RefVol différent
            .Select(volAvecPassagers => volAvecPassagers.First()) //On prend alors le 1er élément de chacun de ces tableaux/listes
            ;

            Debug.ShowData(volsAvecPassagers.ToList());
            chrono.StopAndShowDuration();

            Console.ReadKey();
        }


        public void TestGroupJoin_Vols_AvecLeursPassagers() //Version simplifiée pour faire ce que se débat à faire TestJoin_Vols_AvecLeursPassagers_Avec_GroupBy_Select() ci-dessus
        {
            Console.WriteLine("\n\n\n\n----- TestGroupJoin_Vols_AvecLeursPassagers    (  Vol   1------>*   Passager)   (moins rapide) -----\n");

            var passagers = dataSource.GetPassagers();
            var vols = dataSource.GetVols();

            chrono.Start();
            var volsAvecPassagers = vols.GroupJoin(
                passagers,
                vol => vol.Ref,                            //Jointure sur vol.Ref == passager.RefVol
                passager => passager.RefVol,

                (vol, passagers) => //On a direct notre liste de passagers !  (  Vol   1------>*   Passager)
                    new
                    {
                        RefVol = vol.Ref,
                        DateProchainDepart = vol.DateProchainDepart.ToString(), //Juste pour le ToString() permettant un formatage correct de la Date lors de l'affichage
                        Passagers = passagers
                    }

            );

            Debug.ShowData(volsAvecPassagers.ToList());
            chrono.StopAndShowDuration();

            Console.ReadKey();
        }

    }
}
#pragma warning restore S125 // Sections of code should not be commented out