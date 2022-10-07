using System;
using System.Linq;

using Microsoft.EntityFrameworkCore; //Pour la méthode Include() d'un DbSet, ainsi que pour la méthode AsNoTracking().

using Transverse.Common.DebugTools;

using DataAccess;
using Domaine.MyEntities;
using System.Collections.Generic;

namespace ConsolePrj
{
    static class Program
    {
        private static MyApplicationDbContext myDbContext;

        static void Main()
        {
            var myDbContextFactory = new MyApplicationDbContextFactory(appelManuel: true);
            myDbContext = myDbContextFactory.GetDbContext();

            PlaceDataIfBaseVierge();

            DisplayQuelNumeroEstSortiAQuellesDates();
            DisplayListeNumerosSortisAUneDateDonnee();
        }

        private static void DisplayQuelNumeroEstSortiAQuellesDates()
        {
            Console.WriteLine("\n\n*** (TirageLoto *<---->* Numero), DisplayQuelNumeroEstSortiAQuellesDates, tri par : Numero, Date sortie(décroiss.) ***");

            var resultat = myDbContext.Numeros.Select(numero => new //pour chaque Numero possible
            {
                NumeroSorti = numero.Valeur,
                DatesSortie = 
                    myDbContext.NumerosTiragesLoto.Where(nt => nt.NumeroId == numero.Id) //On ne s'intéresse qu'aux records de NumerosTiragesLoto qui sont relatifs à ce numero
                    .Join(myDbContext.TiragesLoto, //Jointure de cet extrait de la table de liaison NumerosTiragesLoto, avec la table TiragesLoto

                        nt => nt.TirageLotoId, //Jointure via :   nt.TirageLotoId == tirage.Id
                        tirage => tirage.Id,


                        (nt, tirage) 
                            => new //Objet qui représentera chaque élément du tableau résultant de ce Join()
                                {
                                    dateSortie = tirage.Date
                                }

                    ) //<<< A ce stade, DatesSortie n'est encore qu'un tableau d'objets : {dateSortie:...}
                    .Select(objet => objet.dateSortie) //On veut que chaque élément du tableau soit en fait une date (tirage.Date), et non plus un objet(de type anonyme).
                    .OrderByDescending(date => date) //Tri en décroissant, des éléments de ce simple tableau.
                    .ToList()
            
            }).OrderBy(obj => obj.NumeroSorti);

            Debug.ShowData(resultat.ToList());
            Console.ReadKey();
        }


        private static void DisplayListeNumerosSortisAUneDateDonnee()
        {
            Console.WriteLine("\n\n\n*** (TirageLoto *<---->* Numero),  DisplayListeNumerosSortisAUneDateDonnee, tri par : Date sortie(décroiss.), Numero ***");

            var resultat = myDbContext.TiragesLoto.Select(tirage => new //pour chaque TirageLoto
            {
                DateTirage = tirage.Date,
                NumerosSortis =
                    myDbContext.NumerosTiragesLoto.Where(nt => nt.TirageLotoId == tirage.Id) //On ne s'intéresse qu'aux records de NumerosTiragesLoto qui sont relatifs à ce tirage
                    .Join(myDbContext.Numeros, //Jointure de cet extrait de la table de liaison NumerosTiragesLoto, avec la table Numeros

                        nt => nt.NumeroId, //Jointure via :   nt.TirageLotoId == tirage.Id
                        numero => numero.Id,


                        (nt, numero)
                            => new //Objet qui représentera chaque élément du tableau résultant de ce Join()
                            {
                                numeroSorti = numero.Valeur
                            }

                    ) //<<< A ce stade, NumerosSortis n'est encore qu'un tableau d'objets : {numeroSorti:...}
                    .Select(objet => objet.numeroSorti) //On veut que chaque élément du tableau soit en fait une Valeur de numéro, et non plus un objet(de type anonyme).
                    .OrderBy(valeurNumero => valeurNumero) //Tri des éléments de ce simple tableau.
                    .ToList()

            }).OrderByDescending(obj => obj.DateTirage);

            Debug.ShowData(resultat.ToList());
            Console.ReadKey();
        }


        private static void PlaceDataIfBaseVierge() //Juste pour alimenter une première fois la base, si pas déjà fait.
        {
            if (estBaseVierge())
            {
                Console.WriteLine(" .Remplissage des tables...");

                myDbContext.Numeros.AddRange(new[]
                {
                    new Numero { Valeur = 1 },
                    new Numero { Valeur = 2 },
                    new Numero { Valeur = 3 },
                    new Numero { Valeur = 4 },
                    new Numero { Valeur = 5 },
                    new Numero { Valeur = 6 },
                    new Numero { Valeur = 7 },
                    new Numero { Valeur = 8 },
                    new Numero { Valeur = 9 },
                    new Numero { Valeur = 10 },
                });


                DateTime date1 = DateTime.Parse("06/03/2004 20:00");
                DateTime date2 = DateTime.Parse("21/08/2016 20:01");
                DateTime date3 = DateTime.Parse("18/02/2017 20:00");
                DateTime date4 = DateTime.Parse("20/03/2006 20:02");
                DateTime date5 = DateTime.Parse("14/06/2011 20:00");
                DateTime date6 = DateTime.Parse("05/01/2022 20:00");
                DateTime date7 = DateTime.Parse("07/09/2010 20:01");
                DateTime date8 = DateTime.Parse("05/07/2003 20:01");
                DateTime date9 = DateTime.Parse("10/08/2014 20:01");
                myDbContext.TiragesLoto.AddRange(new[]
                {
                    new TirageLoto { Date = date1 },
                    new TirageLoto { Date = date2 },
                    new TirageLoto { Date = date3 },
                    new TirageLoto { Date = date4 },
                    new TirageLoto { Date = date5 },
                    new TirageLoto { Date = date6 },
                    new TirageLoto { Date = date7 },
                    new TirageLoto { Date = date8 },
                    new TirageLoto { Date = date9 },
                });

                myDbContext.SaveChanges();


                //La technique juste ci-dessous ne passe pas : erreur à l'exéc. de duplication du champ "Valeur" (UNIQUE) dans la table Numeros !!
                //myDbContext.TiragesLoto.AddRange(new[]
                    //{
                    //    new TirageLoto { Date = date1, Numeros = new[] { new Numero() { Valeur = 6 }, new Numero() { Valeur = 3 }, new Numero() { Valeur = 4 } } },
                    //    new TirageLoto { Date = date2, Numeros = new[] { new Numero() { Valeur = 10 }, new Numero() { Valeur = 8 }, new Numero() { Valeur = 4 } } },
                    //    new TirageLoto { Date = date3, Numeros = new[] { new Numero() { Valeur = 8 }, new Numero() { Valeur = 2 }, new Numero() { Valeur = 7 } } },
                    //    new TirageLoto { Date = date4, Numeros = new[] { new Numero() { Valeur = 2 }, new Numero() { Valeur = 3 }, new Numero() { Valeur = 6 } } },
                    //    new TirageLoto { Date = date5, Numeros = new[] { new Numero() { Valeur = 4 }, new Numero() { Valeur = 6 }, new Numero() { Valeur = 1 } } },
                    //    new TirageLoto { Date = date6, Numeros = new[] { new Numero() { Valeur = 5 }, new Numero() { Valeur = 1 }, new Numero() { Valeur = 2 } } },
                    //    new TirageLoto { Date = date7, Numeros = new[] { new Numero() { Valeur = 7 }, new Numero() { Valeur = 9 }, new Numero() { Valeur = 10 } } },
                    //    new TirageLoto { Date = date8, Numeros = new[] { new Numero() { Valeur = 5 }, new Numero() { Valeur = 7 }, new Numero() { Valeur = 3 } } },
                    //    new TirageLoto { Date = date9, Numeros = new[] { new Numero() { Valeur = 10 }, new Numero() { Valeur = 8 }, new Numero() { Valeur = 4 } } },
                    //});

                //Il faut écrire dans la table intermédiaire SOI-MÊME :
                myDbContext.NumerosTiragesLoto.AddRange(new[]
                {
                    GetNumeroTirageLoto(date1, 6),
                    GetNumeroTirageLoto(date1, 3),
                    GetNumeroTirageLoto(date1, 4),

                    GetNumeroTirageLoto(date2, 10),
                    GetNumeroTirageLoto(date2, 8),
                    GetNumeroTirageLoto(date2, 4),

                    GetNumeroTirageLoto(date3, 8),
                    GetNumeroTirageLoto(date3, 2),
                    GetNumeroTirageLoto(date3, 7),

                    GetNumeroTirageLoto(date4, 2),
                    GetNumeroTirageLoto(date4, 3),
                    GetNumeroTirageLoto(date4, 6),

                    GetNumeroTirageLoto(date5, 4),
                    GetNumeroTirageLoto(date5, 6),
                    GetNumeroTirageLoto(date5, 1),

                    GetNumeroTirageLoto(date6, 5),
                    GetNumeroTirageLoto(date6, 1),
                    GetNumeroTirageLoto(date6, 2),


                    GetNumeroTirageLoto(date7, 7),
                    GetNumeroTirageLoto(date7, 9),
                    GetNumeroTirageLoto(date7, 10),


                    GetNumeroTirageLoto(date8, 5),
                    GetNumeroTirageLoto(date8, 7),
                    GetNumeroTirageLoto(date8, 3),

                    GetNumeroTirageLoto(date9, 10),
                    GetNumeroTirageLoto(date9, 8),
                    GetNumeroTirageLoto(date9, 4),

                });

                myDbContext.SaveChanges();

                Console.WriteLine("\nInit. des data de la base réalisée.\n");

            }
        }

        private static NumeroTirageLoto GetNumeroTirageLoto(DateTime dateTirage, uint valeurNumero)
        {
            return new NumeroTirageLoto
            {
                NumeroId = myDbContext.Numeros.Where(n => n.Valeur == valeurNumero).First().Id,
                TirageLotoId = myDbContext.TiragesLoto.Where(t => t.Date == dateTirage).First().Id
            };
        }

        private static bool estBaseVierge()
        {
            return !myDbContext.NumerosTiragesLoto.Any();
        }

    }
}
