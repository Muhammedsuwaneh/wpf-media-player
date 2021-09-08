using System;

namespace MediaPlayer
{
    public interface IRelayCommand
    {
        event EventHandler CanExecuteChanged;

        bool CanExecute(object parameter);
        void Execute(object parameter);
    }
}