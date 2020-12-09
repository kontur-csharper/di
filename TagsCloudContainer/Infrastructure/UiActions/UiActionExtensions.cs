using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudContainer.Infrastructure.UiActions
{
    public static class UiActionExtensions
    {
        public static ToolStripItem[] ToMenuItems(this IUiAction[] actions)
        {
            var items = actions.GroupBy(a => a.Category)
                .OrderBy(a => a.Key)
                .Select(g => CreateTopLevelMenuItem(g.Key, g.ToList()))
                .Cast<ToolStripItem>()
                .ToArray();
            return items;
        }

        private static ToolStripMenuItem CreateTopLevelMenuItem(MenuCategory category, IList<IUiAction> items)
        {
            var menuItems = items.Select(a => a.ToMenuItem()).ToArray();
            return new ToolStripMenuItem(category.GetDescription(), null, menuItems);
        }

        public static ToolStripItem ToMenuItem(this IUiAction action)
        {
            return
                new ToolStripMenuItem(action.Name, null, (sender, args) => action.Perform())
                {
                    ToolTipText = action.Description,
                    Tag = action
                };
        }
    }
}