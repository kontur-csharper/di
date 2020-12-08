using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloud
{
    public static class UiActionExtensions
    {
        public static ToolStripItem[] ToMenuItems(this IUiAction[] actions)
        {
            return actions
                .GroupBy(x => x.Category)
                .Select(x => CreateTopLevelMenuItem(x.Key, x.ToList()))
                .Cast<ToolStripItem>()
                .ToArray();
        }

        private static ToolStripMenuItem CreateTopLevelMenuItem(string name, IList<IUiAction> items)
        {
            var menuItems = items.Select(a => a.ToMenuItem()).ToArray();
            return new ToolStripMenuItem(name, null, menuItems);
        }
        public static ToolStripItem ToMenuItem(this IUiAction action)
        {
            return new ToolStripMenuItem(action.Name, null, (sender, args) => action.Perform())
            {
                ToolTipText = action.Description,
                Tag = action
            };
        }
    }
}