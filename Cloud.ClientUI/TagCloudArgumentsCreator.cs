using Cloud.ClientUI.ArgumentConverters;
using CloudContainer;

namespace Cloud.ClientUI
{
    public class TagCloudArgumentsCreator
    {
        private readonly IArgumentConverter converter;
        private readonly IArgumentParser parser;

        public TagCloudArgumentsCreator(IArgumentParser parser, IArgumentConverter converter)
        {
            this.parser = parser;
            this.converter = converter;
        }


        public TagCloudArguments GetArguments(string[] args)
        {
            var parsedArguments = parser.Parse(args);
            return converter.ConvertArguments(parsedArguments);
        }
    }
}