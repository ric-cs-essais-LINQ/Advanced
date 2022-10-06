using Data.Interfaces;

namespace Data
{
    public class DataSourceFactory
    {
        public DataSourceFactory()
        {
        }

        public static DataSourceFactory GetInstance()
        {
            return (new DataSourceFactory());
        }


        public IDataSource GetSingleton()
        {
            return (new DataSource1());
        }


    }
}
