using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.TagClouds
{
    public abstract class TagCloud<T> : IEnumerable<T>
    {
        protected List<T> Elements = new List<T>();
        protected Point LeftUp = new Point(int.MaxValue, int.MaxValue);
        protected Point RightDown = new Point(int.MinValue, int.MinValue);
        public int Count => Elements.Count;

        public Point LeftUpBound => new Point(LeftUp.X, LeftUp.Y);
        public Point RightDownBound => new Point(RightDown.X, RightDown.Y);

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual void AddElement(T element)
        {
            Elements.Add(element);
        }

        public void AddElements(IEnumerable<T> rectangles)
        {
            foreach (var rectangle in rectangles)
                AddElement(rectangle);
        }
    }
}
