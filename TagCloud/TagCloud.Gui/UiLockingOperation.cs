using System;
using System.Threading;
using System.Windows.Forms;
using TagCloud.Gui.Utils;

namespace TagCloud.Gui
{
    public class UiLockingOperation : IDisposable
    {
        private readonly ActionDisposable disposer;
        private readonly ProgressBar progressBar;
        private ProgressBarContext? progressBarContext;

        public UiLockingOperation(CancellationToken cancellationToken, ActionDisposable disposer,
            ProgressBar progressBar)
        {
            CancellationToken = cancellationToken;
            this.disposer = disposer;
            this.progressBar = progressBar;
        }

        public CancellationToken CancellationToken { get; }

        public ProgressBarContext GetProgressBarContext(int minValue, int maxValue)
        {
            return progressBarContext = new ProgressBarContext(progressBar, minValue, maxValue);
        }

        public void Dispose()
        {
            if (progressBarContext != null && !progressBarContext.Disposed)
                progressBarContext.Dispose();
            disposer.Dispose();
        }
    }
}