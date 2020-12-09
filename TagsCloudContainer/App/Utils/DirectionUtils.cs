namespace TagsCloudContainer.App.Utils
{
    public static class DirectionUtils
    {
        public static readonly DirectionToMove[] Directions =
        {
            DirectionToMove.Up,
            DirectionToMove.Down,
            DirectionToMove.Left,
            DirectionToMove.Right
        };
    }

    public enum DirectionToMove
    {
        Up,
        Down,
        Left,
        Right
    }
}