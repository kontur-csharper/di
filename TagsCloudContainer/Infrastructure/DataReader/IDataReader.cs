using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure.DataReader
{
    public interface IDataReader
    {
        public IEnumerable<string> ReadLines();
    }
}