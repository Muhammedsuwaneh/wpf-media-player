using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System;
using Autofac;
using System.Collections.ObjectModel;

namespace MediaPlayer
{
    /// <summary>
    /// Main window view model
    /// </summary>
    public class ShellViewModel : ObservableObject
    {
        #region Private Properties

        /// <summary>
        /// File where recent media is stored 
        /// </summary>
        private string RecentMediaFileName { get; set; } = @"Recent.dat";
        
        /// <summary>
        /// File where a user's playlist is saved 
        /// </summary>
        private string PlaylistFileName { get; set; } = @"Playlist.txt";

        /// <summary>
        /// Current window to be set
        /// </summary>
        private Window _window { get; set; }

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
        /// Store current media path for future use 
        /// </summary>
        private string CurrentMediaPath { get; set; } = string.Empty;

        /// <summary>
        /// Sets the window radius 
        /// </summary>
        private int _WindowRadius { get; set; } = 20;

        #endregion

        #region Public Properties 

        /// <summary>
        /// Recently played media files property 
        /// </summary>
        public ObservableCollection<MediaFile> _RecentMediaFiles { get; set; } 
        
        /// <summary>
        /// Recently played media to be bound 
        /// </summary>
        public ObservableCollection<MediaFile> RecentMediaFiles
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

        private bool _RecentIsNotEmpty { get; set; } = false;

        public bool RecentIsNotEmpty
        {
            get { return _RecentIsNotEmpty; }
            set
            {
                if(_RecentIsNotEmpty != value)
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


        /// <summary>
        /// the current content control view 
        /// </summary>
        public CurrentViewType _CurrentView { get; set; } = CurrentViewType.MediaBackground;
        public CurrentViewType CurrentView
        {
            get { return _CurrentView; }
            set
            {
                if(_CurrentView != value)
                {
                    _CurrentView = value;
                    OnPropertyChanged("CurrentView");
                }
            }
        }

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

        /// <summary>
        /// Play recent media command 
        /// </summary>
        private ICommand PlayRecentCommand { get; set; }

        #endregion

        #region Constructor 

        /// <summary>
        /// ShellView Constructor
        /// </summary>
        /// <param name="window"></param>
        public ShellViewModel(Window window)
        {
            _window = window;

            // listen out for window resizing 
            _window.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(WindowCornerRadius));
                OnPropertyChanged(nameof(WindowRadius));
            };

            // helps in resizing window 
            var resizer = new WindowResizer(_window);

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
                    if(x.ClickCount == 2)
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
                        LoadMedia();
                    }

                }));
            }
        }

        /// <summary>
        /// Plays the recent media 
        /// </summary>
        public ICommand PlayRecent_Click
        {
            get
            {
                return PlayRecentCommand ?? (PlayRecentCommand = new RelayCommand<MouseButtonEventArgs>(x =>
                {
                    _window.Close();
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
                    CurrentMediaPath = fileName;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Loads the recently with file path data 
        /// </summary>
        private void ReloadRecentlyPlayed()
        {
            /// Get the recent data 
            _RecentMediaFiles = DataAccessFactory.GetDataAccessInstance().ReadFromFile(RecentMediaFileName);

            // check if recent data is empty 
            _RecentIsNotEmpty = _RecentMediaFiles.Count > 0;
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
        private void LoadMedia()
        {
            // set media uri 
            MediaViewModel._MediaSource = new Uri(CurrentMediaPath);

            // Save current media to recently saved
            DataAccessFactory.GetDataAccessInstance().WriteToFile(RecentMediaFileName, CurrentMediaPath);

            // Reloads the recently played media 
            ReloadRecentlyPlayed();

            // remove current media view for replays or viewing other media 
            if (_CurrentView == CurrentViewType.Media)
                _CurrentView = CurrentViewType.MediaBackground;

            // Switch to media view 
            _CurrentView = CurrentViewType.Media;
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
