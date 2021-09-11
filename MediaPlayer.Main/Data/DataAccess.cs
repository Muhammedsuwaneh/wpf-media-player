using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace MediaPlayer
{
    /// <summary>
    /// This class reads and writes data from the media player 
    /// Data includes media filepaths user's playlist and recently played 
    /// </summary>
    public class DataAccess : IDataAccess
    {
        /// <summary>
        /// Reads from a file and returns the path of the files 
        /// stored there 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> ReadFromFile(string filename)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes to a specific file 
        /// </summary>
        /// <param name="filename">name of file to write to</param>
        /// <param name="data">data to write</param>
        public void WriteToFile(string filename, string data)
        {
            FileStream stream = null;

            stream = new FileStream(filename, FileMode.OpenOrCreate);

            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.WriteLine(data);
            };
        }
    }
}
