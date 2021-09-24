using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace MediaPlayer
{
    public interface IDataAccess
    {
        ObservableCollection<IShellViewModel> ReadFromFile(string filename, Window shellview);
        void WriteToFile(string filename, string data);
    }
}