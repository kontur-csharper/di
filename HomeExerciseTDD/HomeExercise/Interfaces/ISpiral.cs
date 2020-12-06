using System.Drawing;

namespace HomeExercise
{
    public interface ISpiral
    {
        public Point Center { get; }
        Point GetNextPoint();
    }
}