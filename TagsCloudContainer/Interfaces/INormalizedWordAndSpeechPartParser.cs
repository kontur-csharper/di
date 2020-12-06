namespace TagsCloudContainer.Interfaces
{
    public interface INormalizedWordAndSpeechPartParser
    {
        string[] ParseToNormalizedWordAndPartSpeech(string text);
    }
}