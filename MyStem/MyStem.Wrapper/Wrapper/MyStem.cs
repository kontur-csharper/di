using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MyStem.Wrapper.Wrapper
{
    //ТУДУ переписать чтобы каждый раз не стартовать процесс по новой
    internal sealed class MyStem : IMyStem
    {
        private readonly Func<Process> processFactory;

        public MyStem(string exePath, string launchArgs)
        {
            processFactory = () => CreateProcess(exePath, launchArgs);
        }

        public string GetResponse(string text)
        {
            using var process = processFactory.Invoke();
            return GetResults(process, text);
        }

        private static string GetResults(Process executingProcess, string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);

            executingProcess.StandardInput.BaseStream.Write(bytes, 0, bytes.Length);
            executingProcess.StandardInput.Flush();
            executingProcess.StandardInput.Close();

            var result = executingProcess.StandardOutput.ReadToEnd();
            executingProcess.WaitForExit();
            return result;
        }

        private static Process CreateProcess(string exePath, string launchArgs)
        {
            if (!File.Exists(exePath))
                throw new FileNotFoundException($"MyStem executable cannot be found on path {exePath}");

            var startedProcess = Process.Start(new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = launchArgs,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                StandardOutputEncoding = Encoding.UTF8,
                StandardInputEncoding = Encoding.UTF8,
            });
            return startedProcess;
        }
    }
}