using System.Collections.Generic;

using Entites;

namespace Data.Interfaces
{
    public interface IDataSource
    {
        IList<Passager> GetPassagers();
        IList<Vol> GetVols();
    }
}
