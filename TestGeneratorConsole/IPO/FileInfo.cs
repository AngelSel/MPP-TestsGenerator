namespace TestGeneratorConsole
{
    public class FileInfo
    {
        public string Path { get; }

        public string Content { get; }

        public FileInfo(string path, string content)
        {
            Path = path;
            Content = content;
        }
    }
}
