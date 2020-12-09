namespace TagsCloudContainer.Infrastructure.DataReader
{
    public interface IDataReaderFactory
    {
        public IDataReader CreateDataReader();
    }
}