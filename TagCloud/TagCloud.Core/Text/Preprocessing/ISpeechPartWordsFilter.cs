using System.Collections.Generic;
using MyStem.Wrapper.Workers.Grammar.Parsing.Models;

namespace TagCloud.Core.Text.Preprocessing
{
    public interface ISpeechPartWordsFilter
    {
        IEnumerable<string> OnlyWithSpeechPart(
            IEnumerable<string> words,
            ISet<MyStemSpeechPart> speechParts);
    }
}