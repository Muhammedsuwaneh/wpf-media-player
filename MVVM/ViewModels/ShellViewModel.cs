using System;
using System.Windows;
using System.Windows.Input;
using Autofac;

namespace MediaPlayer
{
    /// <summary>
    /// Main window view model
    /// </summary>
    public class ShellViewModel : ObservableObject
    {
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
        /// ShellView Constructor
        /// </summary>
        /// <param name="window"></param>
        public ShellViewModel(Window window)
        {
            _window = window;

            // minimize window
            MinimizeCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized );

            // maximize window 
            MaximizeCommand = new RelayCommand(() => _window.WindowState ^= WindowState.Maximized );

            // close window 
            CloseWindowCommand = new RelayCommand(() => _window.Close());

            var resizer = new WindowResizer(_window);
        }
    }
}
