using MyStem.Wrapper.Enums;

namespace MyStem.Wrapper.Wrapper
{
    public interface IMyStemBuilder
    {
        IMyStem Create(MyStemOutputFormat outputFormat, params MyStemOptions[] args);
    }
}