using CloudContainer;

namespace Cloud.ClientUI.ArgumentConverters
{
    public interface IArgumentConverter
    {
        public TagCloudArguments ConvertArguments(Arguments arguments);
    }
}