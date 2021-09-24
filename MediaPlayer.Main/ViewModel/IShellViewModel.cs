using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MediaPlayer
{
    public interface IShellViewModel
    {
        CurrentViewType _CurrentView { get; set; }
        ICommand CloseWindow_Click { get; }
        CurrentViewType CurrentView { get; set; }
        string FilePath { get; set; }
        ICommand MaximizeWindow_Click { get; }
        ICommand MinimizeWindow_Click { get; }
        ICommand OpenFile_Click { get; }
        ICommand PlayRecentCommand { get; }
        bool RecentIsNotEmpty { get; set; }
        Window _window { get; set; }
        ObservableCollection<IShellViewModel> RecentMediaFiles { get; set; }
        ICommand Window_DragClick { get; }
        CornerRadius WindowCornerRadius { get; }
        int WindowRadius { get; set; }

        void LoadMedia();
    }
}