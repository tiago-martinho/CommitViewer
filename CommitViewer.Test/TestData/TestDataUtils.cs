using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Domain.Models;
using Newtonsoft.Json;

namespace CommitViewer.Test.TestData
{
    internal static class TestDataUtils
    {
        internal static StreamReader GetStreamReaderInput(string filePath)
        {
            string fileContent = File.ReadAllText(filePath);
            Stream stream = ConvertToStream(fileContent);
            return new StreamReader(stream);
        }

        internal static ICollection<Commit> GetCommitOutput(string filePath)
        {
            string fileContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ICollection<Commit>>(fileContent);
        }

        internal static Stream ConvertToStream(string str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
