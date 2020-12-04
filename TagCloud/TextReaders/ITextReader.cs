using System.Collections.Generic;    

namespace TagCloud.TextReaders
{
    public interface ITextReader
    {
        public List<string> ReadWords();
    }
}
