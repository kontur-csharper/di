using System;
using MyStem.Wrapper.Enums;
using MyStem.Wrapper.Wrapper;

namespace MyStem.Wrapper.Workers.Lemmas
{
    public class Lemmatizer : ILemmatizer
    {
        private readonly IMyStem myStem;

        public Lemmatizer(IMyStemBuilder myStemBuilder)
        {
            myStem = myStemBuilder.Create(MyStemOutputFormat.Text,
                MyStemOptions.WithoutOriginalForm, MyStemOptions.WithContextualDeHomonymy);
        }

        public string[] GetWords(string text)
        {
            var output = myStem.GetResponse(text);
            if (string.IsNullOrWhiteSpace(output))
                return new string[0];
            return output.Substring(1, output.Length - 2)
                .Split(new[] {"}{"}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}