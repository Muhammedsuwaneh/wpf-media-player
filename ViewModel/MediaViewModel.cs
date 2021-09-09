using System;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;

namespace MediaPlayer
{
    /// <summary>
    /// This class is responsible for the media behavior 
    /// </summary>
    public class MediaViewModel : ObservableObject
    {
        #region Public Properties 

        public static string MediaPath { get; set; }

        #endregion

        #region Media Element Properties 

        private static MediaElement mediaElementObject { get; set; }

        public MediaElement MediaElementObject
        {
            get { return mediaElementObject; }
            set
            {
                mediaElementObject = value;
                OnPropertyChanged("MediaElementObject");
            }
        } 

        #endregion

        #region Constructor 

        public MediaViewModel()
        {
            // create media instance 
            MediaElementObject = new MediaElement();

            // load current media to view 
            LoadMedia();

            //// create timer instance 
            //DispatcherTimer Timer = new DispatcherTimer();

            //// start in 1s 
            //Timer.Interval = TimeSpan.FromSeconds(1);

            //// tick clock 
            //Timer.Tick += Timer_Tick;

            //// Start clock
            //Timer.Start();
        }

        #endregion

        #region Media Controls 

        /// <summary>
        /// Plays current media
        /// </summary>
        public static void Play()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Pauses current media 
        /// </summary>
        public static void Pause()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stops current media
        /// </summary>
        public static void Stop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Skips to the next media on playlist
        /// </summary>
        public static void Next()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Plays previous media on playlist
        /// </summary>
        public static void Previous()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Skips media's duration 
        /// </summary>
        public static void Forward()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Rewinds media's duration
        /// </summary>
        public static void Rewind()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Increases the media's volume 
        /// </summary>
        public static void VolumeUp()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Decreases the media's volume 
        /// </summary>
        public static void VolumeDown()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helpers 

        /// <summary>
        /// Loads the current media 
        /// </summary>
        private void LoadMedia()
        {
            // get media source 
            mediaElementObject.Source = new Uri(MediaPath);

            // manually load media 
            mediaElementObject.LoadedBehavior = MediaState.Manual;

            // set aspect ratio
            mediaElementObject.Stretch = Stretch.Uniform;

            // play media 
            mediaElementObject.Play();
        }

        /// <summary>
        /// Runs the media timer and updates media progress slider every second 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves current media to recently played 
        /// </summary>
        private void SaveCurrentMediaToRecentlyPlayed()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
