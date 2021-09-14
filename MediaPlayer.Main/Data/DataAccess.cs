using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace MediaPlayer
{
    /// <summary>
    /// This class reads and writes data from the media player 
    /// Data includes media filepaths user's playlist and recently played 
    /// </summary>
    public class DataAccess : IDataAccess
    {
        #region File Operations
        /// <summary>
        /// Writes to a specific file 
        /// </summary>
        /// <param name="filename">name of file to write to</param>
        /// <param name="data">data to write</param>
        public void WriteToFile(string filename, string data)
        {
            FileStream fileStream = null;

            // check if file even exist before we proceed 
            if (MediaPathAlreadyExist(filename, data)) return;

            fileStream = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.None);

            using(var writer = new BinaryWriter(fileStream))
            {
                writer.Write(data);
            }
        }

        /// <summary>
        /// Reads from a file and returns the path of the files stored there 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public ObservableCollection<MediaFile> ReadFromFile(string filename)
        {
            ObservableCollection<MediaFile> output = new ObservableCollection<MediaFile>();

            if (File.Exists(filename) != false)
            {
                FileInfo file = new FileInfo(filename);

                using(BinaryReader bn = new BinaryReader(file.OpenRead()))
                {
                    string path = bn.ReadString();
                    output.Add(new MediaFile { FilePath = path });
                }
            }

            output = new ObservableCollection<MediaFile>(output.Reverse());

            return output;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Checks if media exist to avoid repetitions
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="mediaPath"></param>
        /// <returns></returns>
        public static bool MediaPathAlreadyExist(string filename, string mediaPath)
        {
            if(File.Exists(filename) == true)
            {
                FileInfo file = new FileInfo(filename);

                using (BinaryReader bn = new BinaryReader(file.OpenRead()))
                {
                    string path = bn.ReadString();

                    if (path == mediaPath) return true;
                }
            } 

            return false;
        }

        #endregion
    }
}
