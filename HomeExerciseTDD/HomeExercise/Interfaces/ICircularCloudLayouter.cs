using System.Drawing;

namespace HomeExercise
{
    public interface ICircularCloudLayouter
    {
        public Point Center { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}