using System.Windows.Forms;

namespace TagsCloudVisualization.Contracts
{
    public interface IMenuItem
    {
        public string MenuAffiliation { get; }
        public string ItemName { get; }
        public DialogResult Execute();
    }
}