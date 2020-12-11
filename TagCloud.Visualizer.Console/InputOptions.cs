using CommandLine;

namespace TagCloud.Visualizer.Console
{
    public class InputOptions
    {
        [Option('f', "fileName")] public string FileName { get; private set; }

        public InputOptions()
        {
            FileName = "input.txt";
        }

        public InputOptions(string fileName)
        {
            FileName = fileName;
        }

        public int ChangeInputOptionsAndReturnExitCode(InputOptions opts)
        {
            FileName = opts.FileName;
            return 0;
        }
    }
}