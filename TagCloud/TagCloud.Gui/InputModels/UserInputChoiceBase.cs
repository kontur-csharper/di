using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Gui.InputModels
{
    public abstract class UserInputChoiceBase<T>
    {
        protected readonly Dictionary<string, UserInputSelectorItem<T>> ItemsByNames;
        
        public UserInputChoiceBase(string description, IEnumerable<UserInputSelectorItem<T>> available)
        {
            ItemsByNames = available.ToDictionary(x => x.Name);
            Description = description;
        }

        public string Description { get; }
        public UserInputSelectorItem<T>[] Available => ItemsByNames.Select(kvp => kvp.Value).ToArray();
    }
}