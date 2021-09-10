using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.IO;
using System;

namespace MediaPlayer
{
    /// <summary>
    /// Main window view model
    /// </summary>
    public class ShellViewModel : ObservableObject
    {
        #region Private Properties

        /// <summary>
        /// Current window to be set
        /// </summary>
        private Window _window { get; set; }

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
        /// Current media path
        /// </summary>
        private string MediaPath { get; set; }

        /// <summary>
        /// Store current media path for future use 
        /// </summary>
        private Uri CurrentMediaPath { get; set; }

        /// <summary>
        /// Sets the window radius 
        /// </summary>
        private int _WindowRadius { get; set; } = 20;

        /// <summary>
        /// Selected Media File Path
        /// </summary>
        public string MediaFilePath { get; set; } = string.Empty;

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
        public CurrentViewType CurrentView { get; set; } = CurrentViewType.MediaBackground;

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
                        // set media uri 
                        MediaViewModel.MediaSource = CurrentMediaPath;

                        // Switch to media view 
                        CurrentView = CurrentViewType.Media;
                    }

                }));
            }
        }

        #endregion


        #region Helpers 
    
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
                    CurrentMediaPath = new Uri(fileName);
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
