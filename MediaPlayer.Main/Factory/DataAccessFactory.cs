using System;

namespace MediaPlayer
{
    public class DataAccessFactory : IDataAccessFactory
    {
        IDataAccess _dataAccess;
        public DataAccessFactory(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// Run Writes to the file method 
        /// </summary>
        /// <param name="file">file to write to</param>
        /// <param name="data">data to write</param>
        public void RunWriteToFile(string file, string data)
        {
            _dataAccess.WriteToFile(file, data);
        }

        public string RunReadFromFile(string file)
        {
            throw new NotImplementedException();
        }
    }
}
