using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TagCloud.Gui
{
    public class ProgressBarContext : IDisposable
    {
        private readonly int previousMax;
        private readonly int previousMin;
        private readonly int previousValue;
        private readonly ProgressBarStyle previousStyle;
        private readonly ProgressBar progressBar;

        public int Value
        {
            get => progressBar.Value;
            set => progressBar.Value = value;
        }

        public bool Disposed { get; private set; }

        public ProgressBarContext(ProgressBar progressBar, int minValue, int maxValue)
        {
            previousMin = progressBar.Minimum;
            previousMax = progressBar.Maximum;
            previousValue = progressBar.Value;
            previousStyle = progressBar.Style;

            progressBar.Minimum = minValue;
            progressBar.Maximum = maxValue;
            progressBar.Value = minValue;
            progressBar.Style = ProgressBarStyle.Blocks;

            this.progressBar = progressBar;
        }

        public void Increment()
        {
            //По какой-то причине Increment() кидает InvalidOperationException только при отладке приложения :\\
            if (!Debugger.IsAttached)
                progressBar.Increment(1);
        }

        public void Dispose()
        {
            progressBar.Minimum = previousMin;
            progressBar.Maximum = previousMax;
            progressBar.Value = previousValue;
            progressBar.Style = previousStyle;
            Disposed = true;
        }
    }
}