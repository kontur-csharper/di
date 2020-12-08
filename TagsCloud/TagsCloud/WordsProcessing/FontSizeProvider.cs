namespace TagsCloud.WordsProcessing
{
    public static class FontSizeProvider
    {
        public static float GetFontSize(float defaultFontSize, double wordFrequency) =>
            (1 + (float)wordFrequency) * defaultFontSize;
    }
}