using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MediaPlayer
{
    /// <summary>
    /// Updates UI for binding <see cref="Binding"/>
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged, IObservableObject
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
