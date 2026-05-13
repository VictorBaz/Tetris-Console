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
    
    public static readonly Dictionary<string, string> PieceColors = new()
    {
        { "I", "\u001b[36m" }, 
        { "O", "\u001b[33m" }, 
        { "T", "\u001b[35m" }, 
        { "S", "\u001b[32m" }, 
        { "Z", "\u001b[31m" },
        { "J", "\u001b[34m" }, 
        { "L", "\u001b[38;5;208m" } 
    };
}