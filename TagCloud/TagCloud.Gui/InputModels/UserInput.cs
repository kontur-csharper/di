using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Gui.InputModels
{
    public static class UserInput
    {
        public static UserInputField Field(string description) => new UserInputField(description);

        public static UserInputSizeField Size(string description, bool showAsPoint = false) =>
            new UserInputSizeField(description, showAsPoint);

        public static UserInputMultipleOptionsChoice<TService> MultipleChoice<TService>(
            IDictionary<string, TService> source,
            string description) =>
            new UserInputMultipleOptionsChoice<TService>(description, SelectorItems(source));

        public static UserInputOneOptionChoice<TService> SingleChoice<TService>(
            IDictionary<string, TService> source,
            string description) =>
            new UserInputOneOptionChoice<TService>(description, SelectorItems(source));

        public static UserInputColor Color(Color defaultValue, string description) =>
            new UserInputColor(description, defaultValue);

        private static UserInputSelectorItem<T>[] SelectorItems<T>(IDictionary<string, T> source) =>
            source.Select(x => new UserInputSelectorItem<T>(x.Key, x.Value))
                .OrderBy(x => x.Name)
                .ToArray();

        public static UserInputColorPalette ColorPalette(string description, params Color[] initialValue) =>
            new UserInputColorPalette(description, initialValue);
    }
}