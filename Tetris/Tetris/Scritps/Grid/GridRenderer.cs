using System.Text;
using Tetris.Component;
using Tetris.Piece;

namespace Tetris.Grid;

public class GridRenderer(GameGrid _grid) : IComponent
{
    GameGrid grid = _grid;
    
    private List<IDrawable> tetrominos = [];
    StringBuilder stringBuilder = new();

    public void Start()
    {
        Console.CursorVisible = false;
    }

    public void Update(double deltaTime)
    {
        DrawLayerGrid();
    }

    private void DrawLayerGrid()
    {
        stringBuilder.Clear();
        stringBuilder.Append("\u001b[H"); 

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                if (IsCellContainTetromino(x, y)) 
                {
                    stringBuilder.Append("\u001b[36m[]\u001b[0m"); 
                }
                else if (!grid.IsCellEmpty(x, y)) 
                {
                    stringBuilder.Append("\u001b[37m##\u001b[0m"); 
                }
                else 
                {
                    stringBuilder.Append("\u001b[90m. \u001b[0m");
                }
            }
            stringBuilder.AppendLine();
        }

        Console.Write(stringBuilder.ToString());
    }
    
    private bool IsCellContainTetromino(int x, int y)
    {
        return tetrominos.Any(t => t.GetRelativeBlocks().Any(b => t.X + b.x == x && t.Y + b.y == y));
    }
    
    public void RegisterDrawable(IDrawable drawable) => tetrominos.Add(drawable);

    public void UnregisterDrawable(IDrawable drawable) => tetrominos.Remove(drawable);
}