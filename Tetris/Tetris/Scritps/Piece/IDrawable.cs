namespace Tetris.Piece;

public interface IDrawable
{
    int X { get; }
    int Y { get; }
    
    List<(int x, int y)> GetRelativeBlocks();
}