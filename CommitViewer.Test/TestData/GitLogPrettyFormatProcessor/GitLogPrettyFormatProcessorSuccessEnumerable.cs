using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommitViewer.Test.TestData.GitLogPrettyFormatProcessor
{
    internal class GitLogPrettyFormatProcessorSuccessEnumerable : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                TestDataUtils.GetStreamReaderInput(Path.Combine("TestData", "GitLogPrettyFormatProcessor", "Input", "Input1.txt")),
                TestDataUtils.GetCommitOutput(Path.Combine("TestData", "GitLogPrettyFormatProcessor", "Output", "Output1.json"))
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
