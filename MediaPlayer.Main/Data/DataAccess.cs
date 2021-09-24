using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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

            if(CheckAmountOfItemsInFile(filename) == 5)
            {
                RewriteFile(filename, data);
                return;
            }
            // create a new file if does not exist 
            if (File.Exists(filename) == false)
                fileStream = new FileStream(filename, FileMode.OpenOrCreate);
            // creates an appendable filestream 
            else
                fileStream = new FileStream(filename, FileMode.Append);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                // write to file 
                writer.WriteLine(data);
            }
        }

        /// <summary>
        /// Reads from a file and returns the path of the files stored there 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public ObservableCollection<IShellViewModel> ReadFromFile(string filename, Window shellview)
        {
            ObservableCollection<IShellViewModel> output = new ObservableCollection<IShellViewModel>();

            if (File.Exists(filename) != false)
            {
                string[] data = File.ReadAllLines(filename);

                foreach (string line in data)
                {
                    IShellViewModel shell = new ShellViewModel(line);

                    shell._window = shellview;

                    output.Add(shell);
                }

                output = new ObservableCollection<IShellViewModel>(output.Reverse());
            }

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
                string[] data = File.ReadAllLines(filename);

                foreach (string line in data)
                {
                    if (line == mediaPath) return true;
                }
            } 

            return false;
        }

        /// <summary>
        /// Returns the amount of items in a file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static int CheckAmountOfItemsInFile(string filename)
        {
            if(File.Exists(filename))
            {
                string[] data = File.ReadAllLines(filename);

                return data.Length;
            }

            return 0;
        }

        /// <summary>
        /// Replaces the first recently played media in file 
        /// </summary>
        /// <param name="filename"></param>
        public static void RewriteFile(string filename, string data)
        {
            if(File.Exists(filename))
            {
                // create a new temp storage for media paths 
                List<string> oldData = new List<string>();

                // store recent data to a temp file
                oldData.AddRange(File.ReadAllLines(filename));

                // remove first recent file 
                oldData.RemoveAt(0);

                // add new recent media path
                oldData.Add(data);

                // rewrite file 
                File.WriteAllLines(filename, oldData);
            }
        }

        #endregion
    }
}
