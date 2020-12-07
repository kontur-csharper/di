namespace TagCloud.Gui.InputModels
{
    public class UserInputSelectorItem<T>
    {
        public bool IsEmpty { get; }
        
        public UserInputSelectorItem(string name, T value)
        {
            Name = name;
            Value = value;
            IsEmpty = false;
        }

        private UserInputSelectorItem()
        {
            Name = "EMPTY";
            Value = default;
            IsEmpty = true;
        }

        public string Name { get; }
        public T Value { get; }
        
        public static UserInputSelectorItem<T> Empty => new UserInputSelectorItem<T>();
    }
}