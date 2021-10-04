using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System;
using System.Windows.Media;
using Autofac;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MediaPlayer
{
    /// <summary>
    /// Main window view model
    /// </summary>
    public class ShellViewModel : ObservableObject, IShellViewModel
    {
        #region Private Properties

        /// <summary>
        /// File where recent media is stored 
        /// </summary>
        private string RecentMediaFileName { get; set; } = @"Recent.dat";

        /// <summary>
        /// Current window to be set
        /// </summary>
        public Window _window { get; set; }

        /// <summary>
        /// Supported media 
        /// </summary>
        private static string[] SupportedFileTypes = new string[]
        {
            ".mp3",
            ".mpg",
            ".mpeg",
            ".mp4",
            ".ts",
            ".mkv",
        };

        /// <summary>
        /// Sets the window radius 
        /// </summary>
        private int _WindowRadius { get; set; } = 20;

        #endregion

        #region Public Properties 

        private int currentVolumePosition { get; set; } = 0;
        private int oldVolumePosition { get; set; } = 0;

        /// <summary>
        /// Media background visibility 
        /// </summary>
        private Visibility _BackgroundVisibility { get; set; } = Visibility.Visible;
        public Visibility BackgroundVisibility
        {
            get { return _BackgroundVisibility; }
            set
            {
                if (_BackgroundVisibility != value)
                {
                    _BackgroundVisibility = value;
                    OnPropertyChanged("MediaVisibility");
                }
            }
        }

        private Visibility _MediaVisibility { get; set; } = Visibility.Collapsed;
        public Visibility MediaVisibility
        {
            get { return _MediaVisibility; }
            set
            {
                if (_MediaVisibility != value)
                {
                    _MediaVisibility = value;
                    OnPropertyChanged("MediaVisibility");
                }
            }
        }
        public string MediaUrlToBeStored { get; set; }

        private MediaState _Load { get; set; } = MediaState.Manual;
        public MediaState Load
        {
            get { return _Load; }
            set
            {
                if (_Load != value)
                {
                    _Load = value;
                    OnPropertyChanged("Load");
                }
            }
        }

        private Stretch _AspectRatio { get; set; } = Stretch.Uniform;
        public Stretch AspectRatio
        {
            get { return _AspectRatio; }
            set
            {
                if (_AspectRatio != value)
                {
                    _AspectRatio = value;
                    OnPropertyChanged("AspectRatio");
                }
            }
        }

        /// <summary>
        /// Current loaded media source
        /// </summary>
        private Uri _MediaSource { get; set; }

        public Uri MediaSource
        {
            get { return _MediaSource; }
            set
            {
                if (_MediaSource != value)
                {
                    _MediaSource = value;
                    OnPropertyChanged("MediaSource");
                }
            }
        }

        /// <summary>
        /// Stores the old media volume in memory in
        /// for later use once we unmute the media 
        /// </summary>
        private double OldMediaVolume { get; set; }

        /// <summary>
        /// default media volume
        /// </summary>
        private double _MediaVolume { get; set; } = 30;
        public double MediaVolume
        {
            get { return _MediaVolume; }
            set
            {
                if (_MediaVolume != value)
                {
                    _MediaVolume = value;
                    OnPropertyChanged("MediaVolume");
                }
            }
        }

        private bool MediaIsPlaying { get; set; } = false;

        /// <summary>
        /// Recently played media files property 
        /// </summary>
        //private ObservableCollection<MediaFile> _RecentMediaFiles { get; set; }
        private ObservableCollection<IShellViewModel> _RecentMediaFiles { get; set; }

        /// <summary>
        /// Recently played media to be bound 
        /// </summary>
        public ObservableCollection<IShellViewModel> RecentMediaFiles
        {
            get
            {
                return _RecentMediaFiles;
            }

            set
            {
                if (_RecentMediaFiles != value)
                {
                    _RecentMediaFiles = value;
                    OnPropertyChanged("RecentMediaFiles");
                }
            }
        }

        /// <summary>
        /// Media Volume Control Collection
        /// </summary>
        private ObservableCollection<VolumeControl> _VolumeControlHeights { get; set; }
        public ObservableCollection<VolumeControl> VolumeControlHeights
        {
            get
            {
                return _VolumeControlHeights;
            }

            set
            {
                if (_VolumeControlHeights != value)
                {
                    _VolumeControlHeights = value;
                    OnPropertyChanged("VolumeControlHeights");
                }
            }
        }

        SolidColorBrush VolumeBarColor = (SolidColorBrush)new BrushConverter().ConvertFromString("#0DCCFE");

        private bool _RecentIsNotEmpty { get; set; } = false;

        public bool RecentIsNotEmpty
        {
            get { return _RecentIsNotEmpty; }
            set
            {
                if (_RecentIsNotEmpty != value)
                {
                    _RecentIsNotEmpty = value;
                    OnPropertyChanged("RecentIsNotEmpty");
                }
            }
        }

        #endregion

        #region Public Window Properties

        /// <summary>
        ///   Radius of edges around the window
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return _window.WindowState == WindowState.Maximized ? 0 : _WindowRadius;
            }

            set
            {
                _WindowRadius = value;
            }
        }

        /// <summary>
        ///  Corner radius
        /// </summary>
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        #endregion

        #region Commands

        /// <summary>
        /// Close window command  
        /// </summary>
        private ICommand CloseWindowCommand { get; set; }

        /// <summary>
        ///  Maximizes the window
        /// </summary>
        private ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// Minimizes the window 
        /// </summary>
        private ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// Command for dragging window 
        /// </summary>
        private ICommand WindowDragCommand { get; set; }

        /// <summary>
        /// Open File command 
        /// </summary>
        private ICommand OpenFileCommand { get; set; }

        #endregion

        #region Constructor 

        /// <summary>
        /// Default constructor 
        /// </summary>
        public ShellViewModel(Window window)
        {
            _window = window;

            InitWindow();
        }

        public ShellViewModel(string filePath)
        {
            MediaUrlToBeStored = filePath;
        }

        public void WindowsInstance(Window window)
        {
            _window = window;

            InitWindow();
        }

        /// <summary>
        /// Initilaizes window properties 
        /// </summary>
        public void InitWindow()
        {
            // listen out for window resizing 
            _window.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(WindowCornerRadius));
                OnPropertyChanged(nameof(WindowRadius));
            };

            //// helps in resizing window 
            var resizer = new WindowResizer(_window);

            // init volume values 
            _VolumeControlHeights = new ObservableCollection<VolumeControl>();

            _VolumeControlHeights.Add(new VolumeControl { VolumeBarHeight = 10, VolumeBarFill = VolumeBarColor });
            _VolumeControlHeights.Add(new VolumeControl { VolumeBarHeight = 20, VolumeBarFill = VolumeBarColor });
            _VolumeControlHeights.Add(new VolumeControl { VolumeBarHeight = 30, VolumeBarFill = VolumeBarColor });
            _VolumeControlHeights.Add(new VolumeControl { VolumeBarHeight = 40 });
            _VolumeControlHeights.Add(new VolumeControl { VolumeBarHeight = 50 });

            currentVolumePosition = 3;

            ReloadRecentlyPlayed();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Implement window dragging 
        /// </summary>
        public ICommand Window_DragClick
        {
            get
            {
                return WindowDragCommand ?? (WindowDragCommand = new RelayCommand<MouseButtonEventArgs>(x =>
                {
                    if (x.ClickCount == 2)
                        _window.WindowState ^= WindowState.Maximized;
                    else
                        _window.DragMove();

                }));
            }
        }
        /// <summary>
        /// Closes the current window 
        /// </summary>
        public ICommand MinimizeWindow_Click
        {
            get
            {
                return MinimizeCommand ?? (MinimizeCommand = new RelayCommand<object>(x =>
                {
                    _window.WindowState = WindowState.Minimized;
                }));
            }
        }

        /// <summary>
        /// Closes the current window 
        /// </summary>
        public ICommand MaximizeWindow_Click
        {
            get
            {
                return MaximizeCommand ?? (MaximizeCommand = new RelayCommand<object>(x =>
                {
                    _window.WindowState ^= WindowState.Maximized;
                }));
            }
        }

        /// <summary>
        /// Closes the current window 
        /// </summary>
        public ICommand CloseWindow_Click
        {
            get
            {
                return CloseWindowCommand ?? (CloseWindowCommand = new RelayCommand<object>(x =>
                {
                    _window.Close();
                }));
            }
        }

        /// <summary>
        /// Open and read media file 
        /// </summary>
        public ICommand OpenFile_Click
        {
            get
            {
                return OpenFileCommand ?? (OpenFileCommand = new RelayCommand<MouseButtonEventArgs>(x =>
                {
                    if (GetMediaFile())
                    {
                        // Loads media 
                        LoadMedia();

                        // Reloads the recently played media 
                        ReloadRecentlyPlayed();
                    }

                }));
            }
        }

        private ICommand _PlayRecentCommand { get; set; }

        /// <summary>
        /// Plays the recent media 
        /// </summary>
        public ICommand PlayRecentCommand
        {
            get
            {
                return _PlayRecentCommand ?? (_PlayRecentCommand = new RelayCommand<object>(x =>
                {
                    LoadMedia();
                }));
            }
        }

        /// <summary>
        /// Increase Volume Commands 
        /// </summary>
        private ICommand _IncreaseVolume { get; set; }
        public ICommand IncreaseVolume
        {
            get
            {
                return _IncreaseVolume ?? (_IncreaseVolume = new RelayCommand<object>(x =>
                {
                    if (currentVolumePosition < VolumeControlHeights.Count)
                    {
                        double currentVolumePositionHeight = _VolumeControlHeights[currentVolumePosition].VolumeBarHeight;

                        _VolumeControlHeights.RemoveAt(currentVolumePosition);

                        _VolumeControlHeights.Insert(currentVolumePosition,
                        new VolumeControl
                        {
                            VolumeBarHeight = currentVolumePositionHeight,
                            VolumeBarFill = VolumeBarColor,
                        });

                        currentVolumePosition++;

                        // increase media volume 
                        _MediaVolume += 10;
                    }

                    else
                    {
                        // set current position to last bar 
                        currentVolumePosition = _VolumeControlHeights.Count;

                        // set media volume to maximum 
                        //if (MediaIsPlaying) MediaViewModel._MediaVolume = 50;

                        // warn user 
                        MessageBox.Show("Higher Volumes may damage your ears");
                    }

                }));
            }
        }

        private ICommand _DecreaseVolume { get; set; }
        public ICommand DecreaseVolume
        {
            get
            {
                return _DecreaseVolume ?? (_DecreaseVolume = new RelayCommand<object>(x =>
                {
                    if (currentVolumePosition - 1 >= 0 && currentVolumePosition - 1 < 5)
                    {
                        // get current bar height 
                        double currentVolumePositionHeight = _VolumeControlHeights[currentVolumePosition - 1].VolumeBarHeight;

                        // remove current bar 
                        _VolumeControlHeights.RemoveAt(currentVolumePosition - 1);

                        // replace current bar with a new volume bar
                        _VolumeControlHeights.Insert(currentVolumePosition - 1,
                        new VolumeControl
                        {
                            VolumeBarHeight = currentVolumePositionHeight,
                            VolumeBarFill = Brushes.White
                        });

                        // decrease count 
                        currentVolumePosition--;

                        // reduce actual media volume 
                        _MediaVolume -= 10;
                    }

                    else
                    {
                        // set current position to first bar
                        currentVolumePosition = 0;

                        // mute media 
                        _MediaVolume = 0;
                    }

                }));
            }
        }

        /// <summary>
        /// Mutes the media element 
        /// </summary>
        private ICommand _MuteMedia { get; set; }

        public ICommand MuteMedia
        {
            get {
                return _MuteMedia ?? (_MuteMedia = new RelayCommand<object>(x =>
                {
                    if (_MediaVolume > 0)
                    {
                        var temp = _VolumeControlHeights;

                        OldMediaVolume = _MediaVolume;
                        oldVolumePosition = currentVolumePosition;

                        _MediaVolume = 0;
                        currentVolumePosition = 0;

                        // clear all highlighted volume bars
                        _VolumeControlHeights.Clear();

                        // insert new unhighlighted volume bars
                        for(int i = 10; i < 60; i += 10)
                        {
                            _VolumeControlHeights.Add(new VolumeControl { VolumeBarHeight = i, VolumeBarFill = Brushes.White });
                        }
                    }

                    else
                    {
                        _MediaVolume = OldMediaVolume;

                        int i = 0, height = 10;

                        // restore volume bars
                        while(i < oldVolumePosition)
                        {
                            _VolumeControlHeights.RemoveAt(i);
                            _VolumeControlHeights.Insert(i, new VolumeControl { VolumeBarHeight = height, VolumeBarFill = VolumeBarColor });

                            i++;
                            height += 10;
                        }

                        currentVolumePosition = oldVolumePosition;
                    }
                }));
            }
        }

        #endregion


        #region Helpers 

        /// <summary>
        /// Obtains the selected media file 
        /// </summary>
        /// <returns></returns>
        private bool GetMediaFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg;*.mp4;*.ts;*.mkv)|*.mp3;*.mpg;*.mpeg;*.mp4;*.ts;*.mkv";

            if (fileDialog.ShowDialog() == true)
            {
                string fileName = fileDialog.FileName;

                if (CheckForEmptyFilePaths(fileName) == true)
                {
                    // display error window 
                    MessageBox.Show("No file selected");
                    return false;
                }

                // check if file type is supported 
                if (CheckForUnsupportedFileTypes(fileName) == false)
                {
                    // display error window 
                    MessageBox.Show("Selected File type is not supported");
                    return false;
                }

                else
                {
                    MediaUrlToBeStored = fileName;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// checks if current selected file is an empty file or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CheckForEmptyFilePaths(string file)
        {
            return string.IsNullOrEmpty(file);
        }

        /// <summary>
        /// Loads the media and switches the media view 
        /// </summary>
        public void LoadMedia()
        {
            _MediaSource = new Uri(MediaUrlToBeStored);

            // Save current media to recently saved
            DataAccessFactory.GetDataAccessInstance().WriteToFile(RecentMediaFileName, MediaUrlToBeStored);

            // display media 
            if (_MediaVisibility != Visibility.Visible)
                _MediaVisibility = Visibility.Visible;

            // play current media 
            _Load = MediaState.Play;

            // hide background 
            if (_BackgroundVisibility != Visibility.Collapsed)
                _BackgroundVisibility = Visibility.Collapsed;

            MediaIsPlaying = true;
        }

        /// <summary>
        /// Loads the recently with file path data 
        /// </summary>
        private void ReloadRecentlyPlayed()
        {
            /// Get the recent data 
            _RecentMediaFiles = DataAccessFactory.GetDataAccessInstance().ReadFromFile(RecentMediaFileName, _window);

            // check if recent data is empty 
            _RecentIsNotEmpty = _RecentMediaFiles.Count > 0;
        }

        /// <summary>
        /// Check for unsupported file types 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool CheckForUnsupportedFileTypes(string file)
        {
            return SupportedFileTypes.Contains(Path.GetExtension(file));
        }

        #endregion
    }
}
