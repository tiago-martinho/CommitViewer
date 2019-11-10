using System.Collections.Generic;
using System.IO;
using CommitViewer.CommitProcessors;
using CommitViewer.Test.TestData.GitLogProcessor;
using Domain.Models;
using Newtonsoft.Json;
using Xunit;

namespace CommitViewer.Test
{
    public class GitLogProcessorTest
    {

        [Theory]
        [ClassData(typeof(GitLogProcessorSuccessEnumerable))]
        public void ProcessCommitStreamIsSuccessfulTest(TextReader textReader, IEnumerable<Commit> expectedOutput)
        {
            ICommitProcessor commitProcessor = new GitLogProcessor();
            IEnumerable<Commit> result = commitProcessor.ProcessCommitStream(textReader);
            Assert.Equal(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(result));
        }
    }
}
