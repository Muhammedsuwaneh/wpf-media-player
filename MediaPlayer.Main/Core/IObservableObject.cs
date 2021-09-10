using System.ComponentModel;

namespace MediaPlayer
{
    public interface IObservableObject
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
}