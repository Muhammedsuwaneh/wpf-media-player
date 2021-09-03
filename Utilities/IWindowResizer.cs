using System;

namespace MediaPlayer
{
    /// <summary>
    /// Helps in resizing the window 
    /// </summary>
    public interface IWindowResizer
    {
        event Action<WindowDockPosition> WindowDockChanged;
    }
}