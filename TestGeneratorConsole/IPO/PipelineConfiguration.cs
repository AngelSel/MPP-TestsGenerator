namespace TestGeneratorConsole
{
    public class PipelineConfiguration
    {
        public int MaxReadingTasks { get; }

        public int MaxProccessingTasks { get; }

        public int MaxWritingTasks { get; }

        public PipelineConfiguration(int maxReadingTasks, int maxProccessingTasks, int maxWritingTasks)
        {
            MaxReadingTasks = maxReadingTasks;
            MaxProccessingTasks = maxProccessingTasks;
            MaxWritingTasks = maxProccessingTasks;
        }
    }
}
