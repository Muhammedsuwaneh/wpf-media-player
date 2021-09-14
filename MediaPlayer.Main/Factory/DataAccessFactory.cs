using System;

namespace MediaPlayer
{
    public static class DataAccessFactory
    {
        public static IDataAccess GetDataAccessInstance()
        {
            return new DataAccess();
        }
    }
}
