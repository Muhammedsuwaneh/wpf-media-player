using System.Collections.Generic;

namespace MediaPlayer
{
    public interface IDataAccess
    {
        List<string> ReadFromFile(string filename);
        void WriteToFile(string filename, string data);
    }
}