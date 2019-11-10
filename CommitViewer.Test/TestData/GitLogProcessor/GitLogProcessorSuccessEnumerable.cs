using System.Collections;
using System.Collections.Generic;
using System.IO;
using Domain.Models;
using Newtonsoft.Json;

namespace CommitViewer.Test.TestData.GitLogProcessor
{
    internal class GitLogProcessorSuccessEnumerable : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                TestDataUtils.GetStreamReaderInput(Path.Combine("TestData", "GitLogProcessor", "Input", "Input1.txt")), 
                TestDataUtils.GetCommitOutput(Path.Combine("TestData", "GitLogProcessor", "Output", "Output1.json"))
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
