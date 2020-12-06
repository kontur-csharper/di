using MyStemWrapper;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class MyStemConverter : ITextConverter
    {
        private readonly MyStem _myStem;

        public MyStemConverter(string pathToMyStem, string parameters)
        {
            _myStem = new MyStem
            {
                PathToMyStem = pathToMyStem,
                Parameters = parameters
            };
        }

        public string ConvertTextToCertainFormat(string originalText)
        {
            return _myStem.Analysis(originalText);
        }
    }
}