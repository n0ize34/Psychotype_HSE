﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Psychotype_HSE.Models;

namespace Psychotype_HSE.Util
{
    public class PythonRunner
    {
        public static void RunScript(string scriptPath, string pythonPath)
        {
            Process process = new System.Diagnostics.Process();
            ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = pythonPath;
            startInfo.Arguments = scriptPath;
            startInfo.WorkingDirectory =AppSettings.WorkingDir +  "Util\\Scripts\\";
            startInfo.ErrorDialog = true;
            process.StartInfo = startInfo;
            process.Start();
            string s = System.IO.Directory.GetCurrentDirectory();
            Console.WriteLine();
        }
    }
}