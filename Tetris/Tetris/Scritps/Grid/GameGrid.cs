namespace Tetris.Grid;

public class GameGrid
{
    public int Height { get; private set; } = 15;
    public int Width { get; private set; } = 9;

    public int[,] Matrix {get; private set;}

    public GameGrid(int _with = 0, int _height = 0 )
    {
        Matrix = _height is <= 0 or <= 0 ? new int[Height, Width] : new int[_height, _with];
    }

    public bool IsCellEmpty(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height) return false;
        return Matrix[y, x] == 0;
    }
    
    public int CheckAndClearLines()
    {
        int linesCleared = 0;

        for (int y = Height - 1; y >= 0; y--)
        {
            if (!IsLineFull(y)) continue;
            
            ClearLine(y);
            ShiftLinesDown(y);
            y++; 
            linesCleared++;
        }
        return linesCleared;
    }

    private bool IsLineFull(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            if (Matrix[y, x] == 0) return false;
        }
        return true;
    }

    private void ClearLine(int y)
    {
        for (int x = 0; x < Width; x++) Matrix[y, x] = 0;
    }

    private void ShiftLinesDown(int startY)
    {
        for (int y = startY; y > 0; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                Matrix[y, x] = Matrix[y - 1, x];
            }
        }
        
        for (int x = 0; x < Width; x++) Matrix[0, x] = 0;
    }

    public void Clear()
    {
        for (int y = 0; y < Height; y++)
        for (int x = 0; x < Width; x++)
            Matrix[y, x] = 0;
    }
}