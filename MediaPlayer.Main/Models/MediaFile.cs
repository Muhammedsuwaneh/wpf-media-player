namespace MediaPlayer
{
    public class MediaFile
    {
        public string FilePath { get; set; }

        public override string ToString()
        {
            return FilePath.ToString();
        }
    }
}
