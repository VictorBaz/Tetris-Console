using System.Text;
using Tetris.Component;
using Tetris.Piece;

namespace Tetris.Grid;

public class GridRenderer(GameGrid _grid) : IComponent
{
    private readonly GameGrid grid = _grid;
    private Tetromino? _activeTetromino; 
    private readonly StringBuilder _stringBuilder = new();

    public void Start()
    {
        Console.CursorVisible = false;
        Console.Clear(); 
    }

    public void Update(double deltaTime)
    {
        DrawLayerGrid();
    }

    private void DrawLayerGrid()
    {
        _stringBuilder.Clear();
        _stringBuilder.Append("\u001b[H"); 

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                if (IsTetrominoAt(x, y, out string? type))
                {
                    string color = TetrominoData.PieceColors.GetValueOrDefault(type!, "\u001b[37m");
                    _stringBuilder.Append(color).Append("[]").Append("\u001b[0m");
                }
                else if (!grid.IsCellEmpty(x, y)) 
                {
                    _stringBuilder.Append("\u001b[37m##\u001b[0m"); 
                }
                else 
                {
                    _stringBuilder.Append("\u001b[90m. \u001b[0m");
                }
            }
            _stringBuilder.AppendLine();
        }

        Console.Write(_stringBuilder.ToString());
    }

    private bool IsTetrominoAt(int x, int y, out string? type)
    {
        type = null;
        if (_activeTetromino == null) return false;

        var blocks = _activeTetromino.GetRelativeBlocks();
        for (int i = 0; i < blocks.Count; i++)
        {
            if (_activeTetromino.X + blocks[i].x == x && 
                _activeTetromino.Y + blocks[i].y == y)
            {
                type = _activeTetromino.Type;
                return true;
            }
        }
        return false;
    }
    
    public void RegisterDrawable(IDrawable drawable)
    {
        if (drawable is Tetromino t) _activeTetromino = t;
    }

    public void UnregisterDrawable(IDrawable drawable)
    {
        if (_activeTetromino == drawable) _activeTetromino = null;
    }
}