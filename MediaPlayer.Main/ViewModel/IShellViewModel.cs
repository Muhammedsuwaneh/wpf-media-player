using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediaPlayer
{
    public interface IShellViewModel
    {
        string _CurrentPlaybackIcon { get; set; }
        Window _window { get; set; }
        Visibility BackgroundVisibility { get; set; }
        double BarLength { get; set; }
        ICommand CloseWindow_Click { get; }
        string CurrentPlaybackIcon { get; set; }
        string CurrentPlaybackState { get; set; }
        ICommand DecreaseVolume { get; }
        ICommand IncreaseVolume { get; }
        ICommand MaximizeWindow_Click { get; }
        MediaElement MediaPlayerElement { get; set; }
        string MediaUrlToBeStored { get; set; }
        ICommand MinimizeWindow_Click { get; }
        ICommand MuteMedia { get; }
        ICommand OpenFile_Click { get; }
        ICommand PlayCommand { get; }
        ICommand PlayRecentCommand { get; }
        Visibility SliderVisibility { get; set; }
        double ProgressBarLength { get; set; }
        bool RecentIsNotEmpty { get; set; }
        ObservableCollection<IShellViewModel> RecentMediaFiles { get; set; }
        Thickness SliderPosition { get; set; }
        ICommand StopCommand { get; }
        ICommand ForwardCommand { get; }
        ICommand RewindCommand { get; }
        string TimeElasped { get; set; }
        string TotalMediaTime { get; set; }
        ObservableCollection<VolumeControl> VolumeControlHeights { get; set; }
        ICommand Window_DragClick { get; }

        CornerRadius WindowCornerRadius { get; }
        int WindowRadius { get; set; }

        void InitWindow();
        void LoadMedia();
        void WindowsInstance(Window window);
    }
}