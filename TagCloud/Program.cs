using TagCloud.Clients;

namespace TagCloud
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IClient client = new ConsoleClient();
            client.Run();
        }
    }
}
