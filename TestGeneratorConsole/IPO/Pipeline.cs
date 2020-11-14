using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TestGeneratorLibrary;

namespace TestGeneratorConsole
{
    public class Pipeline
    {
        private TestGenerator generator = new TestGenerator();

        private readonly PipelineConfiguration _pipelineConfiguration;

        public Pipeline(PipelineConfiguration pipelineConfiguration)
        {
            _pipelineConfiguration = pipelineConfiguration;
        }

        public async Task Processing(IEnumerable<string> files, string outputDirectory)
        {
            

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            var readingBlock = new TransformBlock<string, string>(
                async path => await File.ReadAllTextAsync(path),
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = _pipelineConfiguration.MaxReadingTasks });


            var processingBlock = new TransformManyBlock<string,TestInfo>(
                async code => await Task.Run(()=> generator.Generate(code)),
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = _pipelineConfiguration.MaxProccessingTasks });

            var writingBlock = new ActionBlock<TestInfo>(
                async fI => await File.WriteAllTextAsync(outputDirectory + fI.TestName+".txt",fI.TestCode),
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = _pipelineConfiguration.MaxWritingTasks });


            readingBlock.LinkTo(processingBlock, linkOptions);
            processingBlock.LinkTo(writingBlock, linkOptions);

            foreach(string file in files)
            {
                readingBlock.Post(file);
            }

            readingBlock.Complete();

            await writingBlock.Completion;

        }

    }
}
