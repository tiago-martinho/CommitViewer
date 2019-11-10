using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommitViewer.CommitProcessors;
using CommitViewer.Test.TestData.GitLogPrettyFormatProcessor;
using Domain.Models;
using Newtonsoft.Json;
using Xunit;

namespace CommitViewer.Test
{
    public class GitLogPrettyFormatProcessorTest
    {
        [Theory]
        [ClassData(typeof(GitLogPrettyFormatProcessorSuccessEnumerable))]
        public void ProcessCommitStreamIsSuccessfulTest(TextReader textReader, IEnumerable<Commit> expectedOutput)
        {
            ICommitProcessor commitProcessor = new GitLogPrettyFormatProcessor();
            IEnumerable<Commit> result = commitProcessor.ProcessCommitStream(textReader);
            Assert.Equal(JsonConvert.SerializeObject(expectedOutput), JsonConvert.SerializeObject(result));
        }
    }
}
