using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.Contracts;

namespace TagsCloudVisualization.Infrastructure.Common
{
    public static class MenuItemExtensions
    {
        public static ToolStripItem[] ToMenuItems(this IEnumerable<IMenuItem> actions) =>
            actions
                .GroupBy(a => a.MenuAffiliation)
                .Select(g => CreateTopLevelMenuItem(g.Key, g.ToList()))
                .Cast<ToolStripItem>()
                .ToArray();

        private static ToolStripMenuItem CreateTopLevelMenuItem(string name, IEnumerable<IMenuItem> items)
        {
            var menuItems = items.Select(a => a.ToMenuItem()).ToArray();
            return new ToolStripMenuItem(name, null, menuItems);
        }

        private static ToolStripItem ToMenuItem(this IMenuItem action) =>
            new ToolStripMenuItem(action.ItemName, null, (sender, args) =>
            {
                var dialogResult =  action.Execute();
                if (dialogResult is DialogResult.OK)
                    SettingsSerializer.Serialize();
            });
    }
}