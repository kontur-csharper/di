using System.Collections.Generic;
using System.Linq;
using MyStem.Wrapper.Enums;
using MyStem.Wrapper.Workers.Grammar.Raw;
using MyStem.Wrapper.Wrapper;
using Newtonsoft.Json;

namespace MyStem.Wrapper.Workers.Grammar
{
    public class GrammarAnalyser : IGrammarAnalyser
    {
        private readonly IMyStem myStem;

        public GrammarAnalyser(IMyStemBuilder myStemBuilder)
        {
            myStem = myStemBuilder.Create(MyStemOutputFormat.Json,
                MyStemOptions.WithoutOriginalForm, MyStemOptions.WithContextualDeHomonymy,
                MyStemOptions.WithGrammarInfo
            );
        }

        public AnalysisResultRaw[] GetRawResult(string text)
        {
            var rawResult = myStem.GetResponse(text);
            return JsonConvert.DeserializeObject<IList<AnalysisResultRaw>>(rawResult).ToArray();
        }
    }
}