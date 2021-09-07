using System;
using System.Windows;
using System.Windows.Input;
using Autofac;
using PropertyChanged;

namespace MediaPlayer
{
    /// <summary>
    /// Main window view model
    /// </summary>
    public class ShellViewModel : ObservableObject
    {
        private ICommand Titlebar_Click;

        /// <summary>
        /// Current window to be set
        /// </summary>
        private Window _window { get; set; }

        /// <summary>
        /// Sets the window radius 
        /// </summary>
        private int _WindowRadius { get; set; } = 20;

        /// <summary>
        /// Handles the window drag event 
        /// </summary>
        public ICommand WindowDragCommad { get; set; }

        /// <summary>
        /// Closes the window 
        /// </summary>
        public ICommand CloseWindowCommand { get; set; }

        /// <summary>
        ///  Maximizes the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// Minimizes the window 
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        public ICommand DragCommand { get; set; }

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

            // minimize window
            MinimizeCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized );

            // maximize window 
            MaximizeCommand = new RelayCommand(() => _window.WindowState ^= WindowState.Maximized );

            // close window 
            CloseWindowCommand = new RelayCommand(() => _window.Close());

            // helps in resizing window 
            var resizer = new WindowResizer(_window);
        }
    }
}
