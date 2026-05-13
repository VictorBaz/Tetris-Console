using Tetris.Component;
using Tetris.Grid;

namespace Tetris.Piece;

public class Tetromino : IComponent, IDrawable
{
    public int X {get; private set;}
    public int Y {get; private set;}

    public List<(int x, int y)> GetRelativeBlocks() => _currentShape;
    
    private List<(int x, int y)> _currentShape;
    
    public string Type { get; private set; }

    private GameGrid gameGrid;
    
    private double _timer;
    private double _inputTimer;

    private bool bufferInputOpen = true;
    
    public Action<Tetromino>? OnLockDown;

    public Tetromino(GameGrid gameGrid, string key)
    {
        this.gameGrid = gameGrid;
        _currentShape = TetrominoData.Shapes.GetValueOrDefault(key) ?? throw new InvalidOperationException();
        Type = key;
        X = gameGrid.Width / 2;
        Y = 1;
    }

    public void Update(double deltaTime)
    {
        TimerMove(deltaTime);
        HandleInput(deltaTime);
    }

    private void TimerMove(double deltaTime)
    {
        _timer += deltaTime;
        
        if (_timer < 1) return;
    
        _timer = 0;

        if (CanMove(0, 1, _currentShape))
        {
            Y++;
        }
        else
        {
            PlaceTetromino(); //TODO ISSUE HERE
        }
    }
    
    private void HandleInput(double deltaTime)
    {
        if (bufferInputOpen)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (CanMove(-1, 0, _currentShape)) X--; 
                        break;
                    case ConsoleKey.RightArrow:
                        if (CanMove(1, 0, _currentShape)) X++;
                        break;
                    case ConsoleKey.DownArrow: //TODO ISSUE HERE
                        if (CanMove(0, 1, _currentShape))
                        {
                            Y++;
                        }
                        else
                        {
                            PlaceTetromino();
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        while (CanMove(0, 1, _currentShape)) { Y++; }
                        PlaceTetromino();
                        break;
                    case ConsoleKey.UpArrow:
                        Rotate();
                        break;
                }
                bufferInputOpen = false;
            }
        }
        
        if (_inputTimer >= 0.1f && !bufferInputOpen) 
        {
            bufferInputOpen = true;
            _inputTimer = 0;
        }
        else
        {
            _inputTimer += deltaTime;
        }
        
    }

    public bool CanMove(int xOffset, int yOffset, List<(int x, int y)> shape)
    {
        foreach (var item in shape)
        {
            int targetX = xOffset + item.x + X;
            int targetY = yOffset + item.y + Y;

            if (!gameGrid.IsCellEmpty(targetX, targetY)) return false;
        }
        return true;
    }

    //TODO SRS
    private void Rotate()
    {
        List<(int, int)> nextShape = new List<(int, int)>(_currentShape.Count);
        
        foreach (var item in _currentShape)
        {
            int oldX = item.x;
            int oldY = item.y;

            int newX = oldY;
            int newY = -oldX;
            
            nextShape.Add((newX, newY));
        }
        
        if (CanMove(0, 0, nextShape))
        {
            _currentShape = nextShape;
        }
    }

    private void PlaceTetromino()
    {
        //if (X == 0 && Y == 0) return;
        
        foreach (var block in _currentShape)
        {
            int targetX = X + block.x;
            int targetY = Y + block.y;

            if (targetX >= 0 && targetX < gameGrid.Width && targetY >= 0 && targetY < gameGrid.Height)
            {
                gameGrid.Matrix[targetY, targetX] = 1; 
            }
        }

        OnLockDown?.Invoke(this);
    }
}