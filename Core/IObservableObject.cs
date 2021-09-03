using System.ComponentModel;

namespace MediaPlayer
{
    /// <summary>
    /// Interfaces for object changes 
    /// </summary>
    public interface IObservableObject
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
}