using CommandLine;

namespace TagCloud
{
    [Verb("center")]
    public class CenterOptions
    {
        public int X { get; set; }
        public int Y { get; set; }

        public CenterOptions(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}