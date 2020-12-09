using System.ComponentModel;

namespace TagsCloudContainer.Infrastructure.UiActions
{
    public enum MenuCategory
    {
        [Description("Файл")]
        File = 1,

        [Description("Настройки")] 
        Settings = 2,

        [Description("Алгоритмы")]
        Algorithms = 3
    }
}