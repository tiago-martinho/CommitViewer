using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace CodacyChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            //variável de ambiente, excepção se o utilizador não tiver o git instalado no sistema e definido como va
            ProcessStartInfo startInfo = new ProcessStartInfo(@"D:\Dev\cmder\vendor\git-for-windows\bin\git.exe");

            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = @"D:\Dev\boot-oauth2-draft";
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            //startInfo.Arguments = "clone http://tk1:tk1@localhost/testrep.git";
            startInfo.Arguments = "log";

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            IList<CommitModel> commitList = new List<CommitModel>();
            string lineVal = process.StandardOutput.ReadLine();

            while (lineVal != null)
            {

                
                lineVal = process.StandardOutput.ReadLine();

            }

            int val = output.Count();
            process.WaitForExit();

        }
    }
}
