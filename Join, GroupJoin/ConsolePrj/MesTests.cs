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


        //=============================== Passagers avec leur vol   (1 passager ---> 1 vol)  ===============================================

        public void Test_Passagers_AvecLeurVol_via_Select_et_Where()
        {
            Console.WriteLine("\n\n\n\n----- Test_Passagers_AvecLeurVol_via_Select_et_Where     (  Vol   1<------*   Passager) -----\n");

            // > Rappel : les new {...}, permettent de décrire des objets libres dits de type : anonyme.

            var passagers = dataSource.GetPassagers();
            var vols = dataSource.GetVols();

            chrono.Start();
            var passagersAvecDetailVol = passagers.Select( passager =>
                new {
                    Numero = passager.Numero,
                    Nom = passager.Nom,
                    Place = passager.Place,
                    RefVol = passager.RefVol,

                    DateProchainDepart = vols.Single(vol => vol.Ref == passager.RefVol).DateProchainDepart
                }
            );

            Debug.ShowData(passagersAvecDetailVol.OrderBy(p => p.RefVol).ToList());
            chrono.StopAndShowDuration();

            Console.ReadKey();
        }


        public void Test_Passagers_AvecLeurVol_via_Join()
        {
            Console.WriteLine("\n\n\n\n----- Test_Passagers_AvecLeurVol_via_Join     (  Vol   1<------*   Passager) -----\n");

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

                //Pour chaque jointure on obtient : d'une part un "Record"(passager) de la table de gauche, de l'autre,  un "Record"(vol) correspondant dans la table liée.
                (passager, vol) => //Projection  (Select sur l'enregistrement résultat de la jointure). 
                    //>>>>>>>>>> Le résultat final du Join, sera un tableau dont chaque élément sera du type déterminé par le new ci-dessous ! <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    new
                    {
                        Numero = passager.Numero,
                        Nom = passager.Nom,
                        Place = passager.Place,
                        RefVol = passager.RefVol,

                        //passager, //<<<Syntaxe équivalant à passager = passager
                        //LeVol = new
                        //{
                        // Ref = vol.Ref,
                        DateProchainDepart = vol.DateProchainDepart.ToString(), //Juste pour le ToString() permettant un formatage correct de la Date lors de l'affichage
                        //}
                    }
            );

            Debug.ShowData(passagersAvecDetailVol.OrderBy(p => p.RefVol).ToList());
            chrono.StopAndShowDuration();

            Console.ReadKey();
        }





        //=============================== Vols avec leurs passagers      (1 vol ---> * passager)   ===============================================

        public void Test_Vols_AvecLeursPassagers_via_Select_et_Where()
        {
            Console.WriteLine("\n\n\n\n----- Test_Vols_AvecLeursPassagers_via_Select_et_Where    (  Vol   1------>*   Passager) -----\n");

            var passagers = dataSource.GetPassagers();
            var vols = dataSource.GetVols();

            chrono.Start();
            /*
             //Ci-dessous OK, MAIS compliqué pour rien :
                var volsAvecPassagers = vols.Join( //Vols InnerJoin Passagers ON vol.Ref = passager.Refvol
                    passagers,
                    vol => vol.Ref,                 //Jointure sur vol.Ref == passager.RefVol
                    passager => passager.RefVol,

                    (vol, passager) => //Projection  (Select sur l'enregistrement(vol+passager) résultat de la jointure)
                        new VolAvecPassagers()
                        {
                            RefVol = vol.Ref,
                            DateProchainDepart = vol.DateProchainDepart.ToString(), //Juste pour le ToString() permettant un formatage correct de la Date lors de l'affichage
                            Passagers = passagers.Where(p => p.RefVol == vol.Ref).ToList() //Pas optimal, on est d'accord :)
                        }

                ) //<< A ce stade on a un tableau d'éléments de type : VolAvecPassagers, mais ce tableau contient des doublons (même RefVol, etc...)
                .GroupBy(volAvecPassagers => volAvecPassagers.RefVol) // << Permet d'obtenir un tableau de sous-tableaux(1 sous-tableau par RefVol), et chacun de ces sous-tableaux contient des éléments (Doublons)
                                                                      // de type VolAvecPassagers, mais ayant en effet exactement le même contenu !
                .Select(volAvecPassagers => volAvecPassagers.First()) //On prend alors le 1er élément de chaque sous-tableau en question.
                ;
            */
            var volsAvecPassagers = vols.Select(vol => new
            {
                RefVol = vol.Ref,
                DateProchainDepart = vol.DateProchainDepart.ToString(), //Juste pour le ToString() permettant un formatage correct de la Date lors de l'affichage
                Passagers = passagers.Where(p => p.RefVol == vol.Ref).ToList()
            });

            Debug.ShowData(volsAvecPassagers.ToList());
            chrono.StopAndShowDuration();

            Console.ReadKey();
        }


        public void Test_Vols_AvecLeursPassagers_via_GroupJoin()
        {
            Console.WriteLine("\n\n\n\n----- Test_Vols_AvecLeursPassagers_via_GroupJoin    (  Vol   1------>*   Passager) -----\n");

            var passagers = dataSource.GetPassagers(); 
            var vols = dataSource.GetVols();

            chrono.Start();
            var volsAvecPassagers = vols.GroupJoin(
                passagers,
                vol => vol.Ref,                            //Jointure sur vol.Ref == passager.RefVol
                passager => passager.RefVol,

                (vol, passagers) => //On a direct notre liste de passagers du dit vol !  (  Vol   1------>*   Passager)
                    //>>>>>>>>>> Le résultat final du GroupJoin, sera un tableau dont chaque élément sera du type déterminé par le new ci-dessous ! <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
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