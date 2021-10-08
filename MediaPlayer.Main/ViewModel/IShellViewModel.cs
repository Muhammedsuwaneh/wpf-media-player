using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MediaPlayer
{
    public interface IShellViewModel
    {
        string _CurrentPlaybackIcon { get; set; }
        Window _window { get; set; }
        Stretch AspectRatio { get; set; }
        Visibility BackgroundVisibility { get; set; }
        ICommand CloseWindow_Click { get; }
        string CurrentPlaybackIcon { get; set; }
        ICommand DecreaseVolume { get; }
        ICommand IncreaseVolume { get; }
        MediaState Load { get; set; }
        ICommand MaximizeWindow_Click { get; }
        Uri MediaSource { get; set; }
        string MediaUrlToBeStored { get; set; }
        Visibility MediaVisibility { get; set; }
        double MediaVolume { get; set; }
        ICommand MinimizeWindow_Click { get; }
        ICommand MuteMedia { get; }
        ICommand OpenFile_Click { get; }
        ICommand PlayCommand { get; }
        ICommand PlayRecentCommand { get; }
        ICommand StopCommand { get; }
        bool RecentIsNotEmpty { get; set; }
        ObservableCollection<IShellViewModel> RecentMediaFiles { get; set; }
        ObservableCollection<VolumeControl> VolumeControlHeights { get; set; }
        ICommand Window_DragClick { get; }
        CornerRadius WindowCornerRadius { get; }
        int WindowRadius { get; set; }

        void InitWindow();
        void LoadMedia();
        void WindowsInstance(Window window);
    }
}