using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TestGeneratorConsole
{
    public class Pipeline
    {

        private readonly PipelineConfiguration _pipelineConfiguration;

        public Pipeline(PipelineConfiguration pipelineConfiguration)
        {
            _pipelineConfiguration = pipelineConfiguration;
        }

        public async Task Processing(IEnumerable<string> files)
        {

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            var readingBlock = new TransformBlock<string, FileInfo>(
                async path => new FileInfo(path, await ReadFile(path)),
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = _pipelineConfiguration.MaxReadingTasks });


            //Block for processing
            /*var processingBlock = new TransformBlock<FileInfo, FileInfo>(
                ,
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = _pipelineConfiguration.MaxProccessingTasks });*/

            var writingBlock = new ActionBlock<FileInfo>(
                async fI => await WriteFile(fI),
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = _pipelineConfiguration.MaxWritingTasks });


            /*readingBlock.LinkTo(processingBlock, linkOptions);
            processingBlock.LinkTo(writingBlock, linkOptions);*/

            foreach(string file in files)
            {
                readingBlock.Post(file);
            }

            readingBlock.Complete();

            await writingBlock.Completion;

        }

        private async Task<string> ReadFile(string path)
        {
            string result;
            using(var sReader = new StreamReader(path))
            {
                result = await sReader.ReadToEndAsync();
            }

            return result;
        }

        private async Task WriteFile(FileInfo file)
        {
            using(var sWriter = new StreamWriter(file.Path))
            {
                await sWriter.WriteAsync(file.Content);
            }

        }
    }
}
