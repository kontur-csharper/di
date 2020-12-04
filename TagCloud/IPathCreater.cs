namespace TagCloud
{
    public interface IPathCreater
    {
        string GetCurrentPath();

        string GetNewPngPath();

        string GetPathToFile(string fileName);
    }
}