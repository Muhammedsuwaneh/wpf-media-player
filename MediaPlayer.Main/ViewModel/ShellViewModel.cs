using System;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using Autofac;
using MediaPlayer.CustomProgressBar;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using MediaPlayer.Dialogs;

namespace MediaPlayer
{
    /// <summary>
    /// Main window view model
    /// </summary>
    public class ShellViewModel : ObservableObject
    {
        #region Private Properties

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
        /// current media time 
        /// </summary>
        private double CurrentTimeSpan { get; set; }

        /// <summary>
        /// Total media time span 
        /// </summary>
        private double TotalTimeSpan { get; set; }

        /// <summary>
        /// Sets the window radius 
        /// </summary>
        private int _WindowRadius { get; set; } = 20;

        /// <summary>
        /// Media Element to load media files
        /// </summary>
        private MediaElement _MediaPlayerElement { get; set; } = new MediaElement();

        public MediaElement MediaPlayerElement
        {
            get { return _MediaPlayerElement; }
            set
            {
                if (_MediaPlayerElement != value)
                {
                    _MediaPlayerElement = value;
                    OnPropertyChanged("MediaPlayerElement");
                }
            }
        }

        /// <summary>
        /// Hides/displays the media player logo 
        /// </summary>
        private Visibility _LogoVisibility { get; set; } = Visibility.Visible;
        public Visibility LogoVisibility
        {
            get { return _LogoVisibility; }
            set
            {
                if(_LogoVisibility != value)
                {
                    _LogoVisibility = value;
                    OnPropertyChanged("LogoVisibility");
                }
            }
        }

        /// <summary>
        /// hides/displays media file name on titlebar  
        /// </summary>
        private Visibility _MediaFileNameVisibility { get; set; } = Visibility.Collapsed;
        public Visibility MediaFileNameVisibility
        {
            get { return _MediaFileNameVisibility; }
            set
            {
                if(_MediaFileNameVisibility != value)
                {
                    _MediaFileNameVisibility = value;
                    OnPropertyChanged("MediaFileNameVisibility");
                }
            }
        }

        private string _MediaFileName { get; set; }
        public string MediaFileName
        {
            get { return _MediaFileName; }
            set
            {
                if(_MediaFileName != value)
                {
                    _MediaFileName = value;
                    OnPropertyChanged("MediaFileName");
                }
            }
        }

        /// <summary>
        /// Progress Bar Length
        /// </summary>
        private double _BarLength { get; set; }
        public double BarLength
        {
            get { return _BarLength; }
            set
            {
                if (_BarLength != value)
                {
                    _BarLength = value;
                    OnPropertyChanged("BarLength");
                }
            }
        }

        /// <summary>
        /// Current media progress length
        /// </summary>
        private double _ProgressBarLength { get; set; }
        public double ProgressBarLength
        {
            get { return _ProgressBarLength; }
            set
            {
                if (_ProgressBarLength != value)
                {
                    _ProgressBarLength = value;
                    OnPropertyChanged("ProgressBarLength");
                }
            }
        }

        /// <summary>
        /// Current volume position
        /// </summary>
        private int currentVolumePosition { get; set; } = 0;

        /// <summary>
        /// old volume position - for future reference
        /// </summary>
        private int oldVolumePosition { get; set; } = 0;

        /// <summary>
        /// Current media progress
        /// </summary>
        private double MediaProgress { get; set; }

        #endregion

        #region Public Properties 

        /// <summary>
        /// Current window to be set
        /// </summary>
        public Window _window { get; set; }

        private string _CurrentPlaybackState { get; set; } = "_Play";

        public string CurrentPlaybackState
        {
            get { return _CurrentPlaybackState; }
            set
            {
                if (_CurrentPlaybackState != value)
                {
                    _CurrentPlaybackState = value;
                    OnPropertyChanged("CurrentPlaybackState");
                }
            }
        }

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
                    OnPropertyChanged("BackgroundVisibility");
                }
            }
        }

        /// <summary>
        /// Progress slider visibility 
        /// </summary>
        private Visibility _SliderVisibility { get; set; } = Visibility.Collapsed;
        public Visibility SliderVisibility
        {
            get { return _SliderVisibility; }
            set
            {
                if (_SliderVisibility != value)
                {
                    _SliderVisibility = value;
                    OnPropertyChanged("SliderVisibility");
                }
            }
        }

        /// <summary>
        /// Media time elasped 
        /// </summary>
        private string _TimeElasped { get; set; } = "--::--";

        public string TimeElasped
        {
            get { return _TimeElasped; }
            set
            {
                if (_TimeElasped != value)
                {
                    _TimeElasped = value;
                    OnPropertyChanged("TimeElasped");
                }
            }
        }

        /// <summary>
        /// Total Media Time 
        /// </summary>
        private string _TotalMediaTime { get; set; } = "--::--";

        public string TotalMediaTime
        {
            get { return _TotalMediaTime; }
            set
            {
                if (_TotalMediaTime != value)
                {
                    _TotalMediaTime = value;
                    OnPropertyChanged("TotalMediaTime");
                }
            }
        }

        public string MediaUrlToBeStored { get; set; }

        /// <summary>
        /// Stores the old media volume in memory in
        /// for later use once we unmute the media 
        /// </summary>
        private double OldMediaVolume { get; set; }

        private Thickness _SliderPosition { get; set; }

        public Thickness SliderPosition
        {
            get { return _SliderPosition; }
            set
            {
                if (_SliderPosition != value)
                {
                    _SliderPosition = value;
                    OnPropertyChanged("SliderPosition");
                }
            }
        }

        private bool MediaIsPlaying { get; set; } = false;


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

        /// <summary>
        /// Color for volume bar background
        /// </summary>
        SolidColorBrush VolumeBarColor = (SolidColorBrush)new BrushConverter().ConvertFromString("#0DCCFE");

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
        /// Stores the current playback icon and it's
        /// updated based on the media state 
        /// </summary>
        public string _CurrentPlaybackIcon { get; set; }
        public string CurrentPlaybackIcon
        {
            get { return _CurrentPlaybackIcon; }
            set
            {
                if (_CurrentPlaybackIcon != value)
                {
                    _CurrentPlaybackIcon = value;
                    OnPropertyChanged("CurrentPlaybackIcon");
                }
            }
        }

        /// <summary>
        /// Vinyl Visibility for audio files
        /// </summary>
        private Visibility _VinylVisibility { get; set; } = Visibility.Collapsed;
        public Visibility VinylVisibility
        {
            get { return _VinylVisibility; }
            set
            {
                if(_VinylVisibility != value)
                {
                    _VinylVisibility = value;
                    OnPropertyChanged("VinylVisibility");
                }
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

            // set default volume bars
            _VolumeControlHeights.Add(new VolumeControl { VolumeBarHeight = 10, VolumeBarFill = VolumeBarColor });
            _VolumeControlHeights.Add(new VolumeControl { VolumeBarHeight = 20, VolumeBarFill = VolumeBarColor });
            _VolumeControlHeights.Add(new VolumeControl { VolumeBarHeight = 30, VolumeBarFill = VolumeBarColor });
            _VolumeControlHeights.Add(new VolumeControl { VolumeBarHeight = 40 });
            _VolumeControlHeights.Add(new VolumeControl { VolumeBarHeight = 50 });

            currentVolumePosition = 3;

            // set default playback play icon to play 
            _CurrentPlaybackIcon = ConvertImagePath("Play.png");

            // controls length of based based on window's size
            _BarLength = _window.Width - 140;

            // set slider position - to be updated once media is played or user manually slides it 
            _SliderPosition = new Thickness(-1, 0, 0, 0);

            // set media element default aspect ratio
            _MediaPlayerElement.Stretch = Stretch.Uniform;

            // set default media volume
            _MediaPlayerElement.Volume = 60;

            // start time dispatcher
            DispatcherTimer Timer = new DispatcherTimer();

            // start in 1s 
            Timer.Interval = TimeSpan.FromSeconds(1);

            // tick clock 
            Timer.Tick += Timer_Tick;

            // Start clock
            Timer.Start();
        }

        /// <summary>
        /// Time ticker - updates the media player every second 
        /// if media player is on play mode 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            // only update progress once media is loaded and playing
            if (_MediaPlayerElement.Source != null && _MediaPlayerElement.IsLoaded == true
                && MediaIsPlaying == true && _MediaPlayerElement.NaturalDuration.HasTimeSpan == true)
            {
                CurrentTimeSpan = _MediaPlayerElement.Position.TotalSeconds;
                TotalTimeSpan = _MediaPlayerElement.NaturalDuration.TimeSpan.TotalSeconds;

                // compute time elasped 
                TimeSpan elasped = TimeSpan.FromSeconds(CurrentTimeSpan);

                // compute total time 
                TimeSpan total = TimeSpan.FromSeconds(TotalTimeSpan);

                // set time elasped format
                _TimeElasped = FormatTime(elasped);

                // set total time format
                _TotalMediaTime = FormatTime(total);

                // get current progress
                MediaProgress = CalculateTimeSpan();

                // update slider position
                if (CurrentTimeSpan >= TotalTimeSpan)
                {
                    // stop video once it is finished 
                    StopMedia();
                }

                else _SliderPosition = new Thickness(MediaProgress, 0, 0, 0);

                // update progress 
                _ProgressBarLength = MediaProgress;
            }
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
                    {
                        _window.WindowState ^= WindowState.Maximized;
                        _BarLength = GetBarLength();
                    }
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

                    _BarLength = GetBarLength();

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
                        // Loads media from file
                        LoadMedia();
                    }

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
                        _MediaPlayerElement.Volume += 20;
                    }

                    else
                    {
                        // set current position to last bar 
                        currentVolumePosition = _VolumeControlHeights.Count;

                        // set media volume to maximum 

                        // warn user 
                        WarningDialog errorDialog = new WarningDialog("Higher Volumes may damage your ears");
                        errorDialog.ShowDialog();
                    }

                }));
            }
        }

        /// <summary>
        /// Decrease volume command 
        /// </summary>
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
                        _MediaPlayerElement.Volume -= 20;
                    }

                    else
                    {
                        // set current position to first bar
                        currentVolumePosition = 0;

                        // mute media 
                        _MediaPlayerElement.Volume = 0;
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
            get
            {
                return _MuteMedia ?? (_MuteMedia = new RelayCommand<object>(x =>
                {
                    if (_MediaPlayerElement.Volume > 0)
                    {
                        var temp = _VolumeControlHeights;

                        OldMediaVolume = _MediaPlayerElement.Volume;
                        oldVolumePosition = currentVolumePosition;

                        _MediaPlayerElement.Volume = 0;
                        currentVolumePosition = 0;

                        // clear all highlighted volume bars
                        _VolumeControlHeights.Clear();

                        // insert new unhighlighted volume bars
                        for (int i = 10; i < 60; i += 10)
                        {
                            _VolumeControlHeights.Add(new VolumeControl { VolumeBarHeight = i, VolumeBarFill = Brushes.White });
                        }
                    }

                    else
                    {
                        _MediaPlayerElement.Volume = OldMediaVolume;

                        int i = 0, height = 10;

                        // restore volume bars
                        while (i < oldVolumePosition)
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

        /// <summary>
        /// Plays/pauses current media 
        /// </summary>
        private ICommand _PlayCommand { get; set; }

        public ICommand PlayCommand
        {
            get
            {
                return _PlayCommand ?? (_PlayCommand = new RelayCommand<object>(x =>
                {

                    // only perform pause and play operations when their a current media 
                    if (_MediaPlayerElement.Source != null)
                    {
                        // pause
                        if (MediaIsPlaying)
                        {
                            // pause media 
                            _MediaPlayerElement.LoadedBehavior = MediaState.Pause;

                            MediaIsPlaying = false;

                            // update playback icon
                            _CurrentPlaybackIcon = ConvertImagePath("Play.png");

                            // current playback menu label 
                            _CurrentPlaybackState = "_Play";

                            // Pause Vinyl animation 
                        }

                        else
                        {
                            // play media 
                            _MediaPlayerElement.LoadedBehavior = MediaState.Play;

                            MediaIsPlaying = true;

                            // update current playback icon
                            _CurrentPlaybackIcon = ConvertImagePath("Pause.png");

                            // update current playback menu label 
                            _CurrentPlaybackState = "_Pause";

                            // Play Vinyl animation 
                        }
                    }

                }));
            }
        }

        /// <summary>
        /// Handles the stop media event 
        /// </summary>
        private ICommand _StopCommand { get; set; }
        public ICommand StopCommand
        {
            get
            {
                return _StopCommand ?? (_StopCommand = new RelayCommand<object>(x =>
                {
                    // close current media 
                    StopMedia();
                }));
            }
        }

        /// <summary>
        /// Rewinds a media 5seconds
        /// </summary>
        private ICommand _RewindCommand { get; set; }
        public ICommand RewindCommand
        {
            get
            {
                return _RewindCommand ?? (_RewindCommand = new RelayCommand<object>(x =>
                {
                    // only rewind if a media playing 
                    if (_MediaPlayerElement.IsLoaded && _MediaPlayerElement.Source != null)
                    {
                        // update progress 
                        CurrentTimeSpan -= 5;

                        if (CurrentTimeSpan <= 0) CurrentTimeSpan = 0;

                        // compute time span for progress bar 
                        MediaProgress = CalculateTimeSpan();

                        _ProgressBarLength = MediaProgress;

                        // update slider position
                        _SliderPosition = new Thickness(MediaProgress, 0, 0, 0);

                        // update media's progress
                        _MediaPlayerElement.Position = TimeSpan.FromSeconds(CurrentTimeSpan);
                    }

                }));
            }
        }

        /// <summary>
        /// Skips a media 5s forward 
        /// </summary>
        private ICommand _ForwardCommand { get; set; }
        public ICommand ForwardCommand
        {
            get
            {
                return _ForwardCommand ?? (_ForwardCommand = new RelayCommand<object>(x =>
                {
                    // only rewind if a media is available 
                    if (_MediaPlayerElement.IsLoaded && _MediaPlayerElement.Source != null)
                    {
                        // update progress 
                        CurrentTimeSpan += 5;

                        // avoid proceesing if current media equals total time span 
                        if (CurrentTimeSpan >= TotalTimeSpan)
                        {
                            StopMedia();
                            return;
                        }

                        // compute time span for progress bar 
                        MediaProgress = CalculateTimeSpan();

                        _ProgressBarLength = MediaProgress;

                        // update slider position
                        _SliderPosition = new Thickness(MediaProgress, 0, 0, 0);

                        // update media's progress
                        _MediaPlayerElement.Position = TimeSpan.FromSeconds(CurrentTimeSpan);
                    }
                }));
            }
        }

        /// <summary>
        /// Displays the helper window
        /// </summary>
        private ICommand _HelperWindowShell { get; set; }

        public ICommand HelperWindowShell
        {
            get
            {
                return _HelperWindowShell ?? (_HelperWindowShell = new RelayCommand<object>(x =>
                {

                    HelperDialog helper = new HelperDialog();
                    helper.ShowDialog();

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
                    ErrorDialog errorDialog = new ErrorDialog("No file selected");
                    errorDialog.ShowDialog();

                    return false;
                }

                // check if file type is supported 
                if (CheckForUnsupportedFileTypes(fileName) == false)
                {
                    // display error window 
                    ErrorDialog errorDialog = new ErrorDialog("Selected File type is not supported");
                    errorDialog.ShowDialog();

                    return false;
                }

                else
                {
                    // obtain media path 
                    MediaUrlToBeStored = fileName;

                    // obtain media filename from path
                    _MediaFileName = Path.GetFileName(fileName);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// returns an asset's entire path 
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        private string ConvertImagePath(string imageName)
        {
            string filePath = "";

            try
            {
                filePath = "pack://application:,,,/MediaPlayer;component/Assets/" + imageName.ToString();
            }
            catch (Exception ex)
            {

                ErrorDialog errorDialog = new ErrorDialog(ex.Message.ToString());
                errorDialog.ShowDialog();
            }

            return filePath;
        }

        /// <summary>
        /// Stops the current media 
        /// </summary>
        private void StopMedia()
        {
            if (_MediaPlayerElement.IsLoaded && _MediaPlayerElement.Source != null)
            {
                ResetMedia();

                // show media background 
                _BackgroundVisibility = Visibility.Visible;

                // hide vinyl
                _VinylVisibility = Visibility.Collapsed;

                // update playback icon 
                _CurrentPlaybackIcon = ConvertImagePath("Play.png");

                _CurrentPlaybackState = "_Play";

                // update volume labels
                _TimeElasped = "--::--";
                _TotalMediaTime = "--::--";

                // update progress bar 
                _ProgressBarLength = .0;
                _SliderPosition = new Thickness(0, 0, 0, 0);

                // hide slider 
                _SliderVisibility = Visibility.Collapsed;

                // hide media file name on title bar 
                _MediaFileNameVisibility = Visibility.Collapsed;

                // show title bar logo
                _LogoVisibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Resets the media properties
        /// </summary>
        private void ResetMedia()
        {
            // stop media 
            _MediaPlayerElement.LoadedBehavior = MediaState.Close;

            // hide media 
            _MediaPlayerElement.Visibility = Visibility.Collapsed;

            // Media is no longer playing 
            MediaIsPlaying = false;

            // reset current time spans
            CurrentTimeSpan = 0;

            // reset total time spans
            TotalTimeSpan = 0;

            // reset media progress
            MediaProgress = 0;

            // reset source
            _MediaPlayerElement.Source = null;
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
        /// Calculates the time span 
        /// </summary>
        /// <returns></returns>
        private double CalculateTimeSpan()
        {
            return (double)(CurrentTimeSpan / TotalTimeSpan) * GetBarLength();
        }

        /// <summary>
        /// Loads the media and switches the media view 
        /// </summary>
        public void LoadMedia()
        {
            _MediaPlayerElement.Source = new Uri(MediaUrlToBeStored);

            // hide background 
            if (_BackgroundVisibility != Visibility.Collapsed)
                _BackgroundVisibility = Visibility.Collapsed;

            // check if current media is an audio 
            if (Path.GetExtension(MediaUrlToBeStored) == ".mp3")
            {
                _VinylVisibility = Visibility.Visible;
                _MediaPlayerElement.Visibility = Visibility.Collapsed;
            }

            // display media - non audio medias 
            if (_MediaPlayerElement.Visibility != Visibility.Visible && 
                (Path.GetExtension(MediaUrlToBeStored) != ".mp3"))
            {
                _VinylVisibility = Visibility.Collapsed;
                _MediaPlayerElement.Visibility = Visibility.Visible;
            }

            // play current media 
            _MediaPlayerElement.LoadedBehavior = MediaState.Play;

            MediaIsPlaying = true;

            _CurrentPlaybackIcon = ConvertImagePath("Pause.png");

            _CurrentPlaybackState = "_Pause";

            // show media file name on title bar 
            _MediaFileNameVisibility = Visibility.Visible;

            // hide title bar logo
            _LogoVisibility = Visibility.Collapsed;

            // show progress slider 
            _SliderVisibility = Visibility.Visible;
        }

        /// <summary>
        /// Formats time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string FormatTime(TimeSpan time)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}",
                    time.Hours, time.Minutes, time.Seconds);
        }

        /// <summary>
        /// returns the progress bar length
        /// </summary>
        /// <returns></returns>
        private double GetBarLength()
        {
            return _window.Width - 140;
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
