using System;
using System.Collections.Generic;

namespace Domaine.MyEntities
{
    public class Numero //Si la présente classe possèdait un membre X de type IList<TirageLoto> et que la classe TirageLoto comportait elle aussi
                        // un membre Y de type IList<Numero>, alors EF comprendrait bien TOUT SEUL qu'il devrait alors automatiquement créer une 
                        // table de liaison, nommée : NumeroTirageLoto, et comportant les champs [X]Id et [Y]Id.
                        // CEPENDANT : à partir de là, je ne suis pas parvenu à remplir mes tables, je peux individuellement remplir la table Numeros,
                        //             et la table TiragesLoto, mais n'ayant pas créer de DbSet sur la table intermédiaire, pas moyen de la remplir.
                        //             Même en s'appuyant sur le renseignement d'une ou des 2 listes évoquées, j'ai des messages d'erreurs de valeurs
                        //             en double (violation Champ "Valeur" unique (cf. NumeroConfig.cs)), etc... .
                        //
                        // Bref, LA BONNE FACON DE FAIRE est la suivante :
                        //   créer à la mano.: une classe NumeroTirageLoto et son DbSet NumerosTiragesLoto,
                        //                     et avoir dans chacune des classes Numero et TirageLoto, un membre
                        //                     de type IList<NumeroTirageLoto> !!
    {
        public int Id { get; set; }

        public uint Valeur { get; set; }

        //public IList<TirageLoto> Tirages { get; init; } // Eh NON !!
        public IList<NumeroTirageLoto> NumerosTiragesLoto { get; set; } //Bonne façon de faire.
    }
}
