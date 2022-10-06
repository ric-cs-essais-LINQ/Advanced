using System;
using System.Collections.Generic;

using Data.Interfaces;
using Entites;

namespace Data
{
    public class DataSource1 : IDataSource
    {
        public DataSource1()
        {

        }

        public IList<Passager> GetPassagers()
        {
            return new List<Passager>()
            {
                new Passager
                {
                    Nom = "PIGEON",
                    Numero = "007100",
                    Place = "A700",

                    RefVol = "Vol710",
                },

                new Passager
                {
                    Nom = "BOURDON",
                    Numero = "008001",
                    Place = "A801",

                    RefVol = "Vol810",
                },

                new Passager
                {
                    Nom = "LAIGLE",
                    Numero = "007101",
                    Place = "A701",

                    RefVol = "Vol710",
                },

                new Passager
                {
                    Nom = "SANZEL",
                    Numero = "008002",
                    Place = "A802",

                    RefVol = "Vol810",
                },

                new Passager
                {
                    Nom = "BOTRIP",
                    Numero = "007102",
                    Place = "A702",

                    RefVol = "Vol710",
                },

                new Passager
                {
                    Nom = "PAPION",
                    Numero = "008003",
                    Place = "A803",

                    RefVol = "Vol810",
                },

                new Passager
                {
                    Nom = "COPIL",
                    Numero = "0001",
                    Place = "A001",

                    RefVol = "Vol910",
                },

                new Passager
                {
                    Nom = "HUBLOT",
                    Numero = "0002",
                    Place = "A002",

                    RefVol = "Vol910",
                },

            };
        }

        public IList<Vol> GetVols()
        {
            return new List<Vol>()
            {
                new Vol
                {
                    Ref = "Vol710",
                    DateProchainDepart = DateTime.Parse("15/03/2022 11:45"),
                },
                new Vol
                {
                    Ref = "Vol810",
                    DateProchainDepart = DateTime.Parse("31/12/2022 16:15"),
                },
                new Vol
                {
                    Ref = "Vol910",
                    DateProchainDepart = DateTime.Parse("12/08/2022 14:00"),
                },
            };
        }
    }
}
