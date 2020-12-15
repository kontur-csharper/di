using TagCloud.Visualization;

namespace TagCloud.Clients
{
    internal interface IClient
    {
        public void Run();

        public void Visualize(string wordsPath, string picturePath);
    }
}
