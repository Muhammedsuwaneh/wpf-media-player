using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MediaPlayer
{
    public interface IDataAccess
    {
        ObservableCollection<MediaFile> ReadFromFile(string filename);
        void WriteToFile(string filename, string data);
    }
}