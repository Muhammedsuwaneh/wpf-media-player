namespace MediaPlayer
{
    public interface IDataAccessFactory
    {
        string RunReadFromFile(string file);
        void RunWriteToFile(string file, string data);
    }
}