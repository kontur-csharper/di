using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        private readonly KochPainter kochPainter;

        public KochFractalAction(KochPainter kochPainter)
        {
            this.kochPainter = kochPainter;
        }

        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";
        public int Order => 30;

        public void Perform()
        {
            kochPainter.Paint();
        }
    }
}