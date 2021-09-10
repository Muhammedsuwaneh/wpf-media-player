using Xunit;
using MediaPlayer;

namespace MediaUnitTests
{
    public class MediaTests
    {
        [Theory]
        [InlineData(".png", false)]
        [InlineData(".jpg", false)]
        public void OpenFile_UnsupportedMediaFormatsShouldFail(string FileExtension, bool expected)
        {

            bool actual = ShellViewModel.CheckForUnsupportedFileTypes(FileExtension);

            // Test 1
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(".mp3", true)]
        [InlineData(".ts", true)]
        public void OpenFile_SupportedMediaFormatsShouldWork(string FileExtension, bool expected)
        {
            bool actual = ShellViewModel.CheckForUnsupportedFileTypes(FileExtension);

            // Test 1
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", true)]
        public void OpenFile_EmptyFilePathsShouldFail(string filePath, bool expected)
        {
            bool actual = ShellViewModel.CheckForEmptyFilePaths(filePath);

            Assert.Equal(expected, actual);
        }
    }
}
