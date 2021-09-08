using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

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

        /// <summary>
        /// Sets the window radius 
        /// </summary>
        private int _WindowRadius { get; set; } = 20;

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
                    if (x.ClickCount == 2)
                    {
                        _window.WindowState ^= WindowState.Maximized;
                    }

                    else _window.DragMove();

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
                    OpenFileDialog fileDialog = new OpenFileDialog();

                    fileDialog.ShowDialog();

                }));
            }
        }

        #endregion
    }
}
