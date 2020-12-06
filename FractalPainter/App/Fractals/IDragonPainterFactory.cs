namespace FractalPainting.App.Fractals
{
    public interface IDragonPainterFactory
    {
        DragonPainter CreatePainter(DragonSettings settings);
    }
}