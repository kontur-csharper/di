using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization.FormAction
{
    internal static class FormActionExtensions
    {
        public static ToolStripItem[] ToMenuItems(this IEnumerable<IFormAction> actions)
        {
            var items = actions.GroupBy(a => a.Category)
                .Select(g => CreateTopLevelMenuItem(g.Key, g.ToList()))
                .Cast<ToolStripItem>()
                .ToArray();
            return items;
        }

        private static ToolStripMenuItem CreateTopLevelMenuItem(string name, IList<IFormAction> items)
        {
            var menuItems = items.Select(a => a.ToMenuItem()).ToArray();
            return new ToolStripMenuItem(name, null, menuItems);
        }

        private static ToolStripItem ToMenuItem(this IFormAction action)
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