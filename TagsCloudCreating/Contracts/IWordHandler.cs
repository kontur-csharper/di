using System.Collections.Generic;

namespace TagsCloudCreating.Contracts
{
    public interface IWordHandler
    {
        public IEnumerable<string> NormalizeAndExcludeBoringWords(IEnumerable<string> sourceWords);
    }
}