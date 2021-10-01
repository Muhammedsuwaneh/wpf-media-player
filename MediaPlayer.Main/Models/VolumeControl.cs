using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MediaPlayer
{
    public class VolumeControl
    {
        public double VolumeBarHeight { get; set; } = 0;
        public Brush VolumeBarFill { get; set; } = Brushes.White;

        public VolumeControl() { }
    }
}
