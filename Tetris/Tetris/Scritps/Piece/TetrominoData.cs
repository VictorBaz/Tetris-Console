namespace Tetris.Piece;

public static class TetrominoData
{
    /// <summary>
    /// Store List of point X and Y to represent each form
    /// </summary>
    public static readonly Dictionary<string, List<(int x, int y)>> Shapes = new() {
        { "I", [(-1, 0), (0, 0), (1, 0), (2, 0)] },
        { "T", [(0, 0), (-1, 0), (1, 0), (0, 1)] },
        { "O", [(0, 0), (1, 0), (0, 1), (1, 1)] },
        { "S", [(0, 0), (1, 0), (0, 1), (-1, 1)] },
        { "Z", [(0, 0), (-1, 0), (0, 1), (1, 1)] },
        { "J", [(0, 0), (-1, 0), (1, 0), (-1, 1)] },
        { "L", [(0, 0), (-1, 0), (1, 0), (1, 1)] },
    };
    
}