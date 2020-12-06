namespace Cloud.ClientUI
{
    public interface IArgumentParser
    {
        public Arguments Parse(string[] args);
    }
}