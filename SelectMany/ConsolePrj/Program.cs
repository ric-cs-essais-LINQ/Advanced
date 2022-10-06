using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Transverse.Common.DebugTools;

namespace zzzzzTestsRapides
{
    class Biblio
    {
        public string Nom { get; set; }
        public ICollection<Livre> Livres { get; set; }
    }
    class Livre
    {
        public uint Id { get; set; }
        public string Titre { get; set; }
    }

    //------------------------------------------------------------------------

    static class Program
    {
        static void Main(string[] args)
        {
            var biblio1 = new Biblio
            {
                Nom = "Biblio numberOne",
                Livres = new Collection<Livre>()
                {
                    new Livre { Id=11, Titre="Biblio1 - Livre1"},
                    new Livre { Id=21, Titre="Biblio1 - Livre2"},
                    new Livre { Id=31, Titre="Biblio1 - Livre3"},
                }
            };

            var biblio2 = new Biblio
            {
                Nom = "Biblio numberTwo",
                Livres = new Collection<Livre>()
                {
                    new Livre { Id=12, Titre="Biblio2 - Livre1"},
                    new Livre { Id=22, Titre="Biblio2 - Livre2"},
                    new Livre { Id=32, Titre="Biblio2 - Livre3"},
                }
            };

            var biblio3 = new Biblio
            {
                Nom = "Biblio numberThree",
                Livres = new Collection<Livre>()
                {
                    new Livre { Id=13, Titre="Biblio3 - Livre1"},
                    new Livre { Id=23, Titre="Biblio3 - Livre2"},
                    new Livre { Id=33, Titre="Biblio3 - Livre3"},
                }
            };



            //---------------------------------------------
            Console.WriteLine("\n---- Liste des biblios ----\n\n"); Console.ReadKey();
            var biblios = new Collection<Biblio>() { biblio3, biblio2, biblio1 };
            Debug.ShowData(biblios);
            Console.WriteLine("\n\n"); Console.ReadKey();


            //-------------------------------------------------------------------------------
            //--- REM.: l'utilisation d'un Select ou SelectMany, s'appelle une Projection ---
            //-------------------------------------------------------------------------------
            Console.WriteLine("\n---- Liste des livres mais par Biblio ----\n\n"); Console.ReadKey();
            var bibliosLivres = biblios.Select(b => b.Livres); //<<<<<<<<<< On obtient quand même une Liste de Biblio, (mais avec uniquement 1 propriété "Livres" pour chacune)
            Debug.ShowData(bibliosLivres);
            Console.WriteLine("\n\n"); Console.ReadKey();

            //---------------------------------------------
            Console.WriteLine("\n---- Liste des livres A PLAT !! (de toutes les Biblios) . (Liste triée par Titre) ----\n\n"); Console.ReadKey();
            var livres = biblios.SelectMany(b => b.Livres).OrderBy(livre => livre.Titre); //<<<<<<<<< Cette fois, on obtient directement une Liste de Livre !! grâce au SelectMany.
            Debug.ShowData(livres);
            Console.WriteLine("\n\n"); Console.ReadKey();

            //----------------------------------------------
            Console.WriteLine("\n\nOK");  Console.ReadKey();
        }
    }
}
