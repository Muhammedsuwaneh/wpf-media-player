using System;
using System.Windows;
using System.Windows.Input;

namespace MediaPlayer
{
    public class MediaFile
    {
        public string FilePath { get; set; }

        public override string ToString()
        {
            return FilePath.ToString();
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
                    //ShellViewModel.LoadMedia(FilePath);
                }));
            }
        }
    }
}
