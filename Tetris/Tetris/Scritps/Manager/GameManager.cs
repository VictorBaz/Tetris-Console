using Tetris.Component;
using Tetris.Grid;
using Tetris.Piece;

namespace Tetris.Manager;

public class GameManager : IComponent
{
    private Engine.Engine _engine;
    
    private GameGrid _grid;
    
    private GridRenderer _renderer;
    
    private Tetromino? currentPiece;
    
    Random random = new();

    private List<string> container = new List<string>();

    public GameManager(Engine.Engine engine, GameGrid grid, GridRenderer renderer)
    {
        _engine = engine;
        _grid = grid;
        _renderer = renderer;
        InitializeContainer();
    }

    private void SpawnNextPiece()
    {
        currentPiece = new Tetromino(_grid, GetRandomPiece());

        if (!currentPiece.CanMove(0, 0, currentPiece.GetRelativeBlocks()))
        {
            SoundManager.PlayDeath();
            GameOver();
            return;
        }
        
        currentPiece.OnLockDown += OnLockDown;

        _engine.AddComponent(currentPiece);
        _renderer.RegisterDrawable(currentPiece);
    }

    public void Start()
    {
        SpawnNextPiece();
        SoundManager.PlayMain();
    }

    private void OnLockDown(Tetromino piece)
    {
        SoundManager.PlayPutPiece();
        _engine.RemoveComponent(piece);
        _renderer.UnregisterDrawable(piece);
        currentPiece.OnLockDown -= OnLockDown;
        
        int scoreMultiplier = _grid.CheckAndClearLines();
        
        if (scoreMultiplier > 0)
        {
            SoundManager.PlayCollect(scoreMultiplier);
        }
        
        SpawnNextPiece(); 
    }

    private string GetRandomPiece()
    {
        if (container.Count == 0) InitializeContainer();
        string piece = container[0];
        container.RemoveAt(0);
        return piece;
    }
    
    private void GameOver()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("GAME OVER");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Press R to restart or escape to quit.");
        Console.ForegroundColor = ConsoleColor.White;

        bool exitPressed = false;
        
        while (!exitPressed)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            
            switch (keyInfo.Key)
            {
                case ConsoleKey.R:
                    RestartGame();
                    exitPressed = true;
                    break;
                case ConsoleKey.Escape:
                    exitPressed = true; 
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void InitializeContainer()
    {
        foreach (var str in TetrominoData.Shapes.Keys)
        {
            container.Add(str);
        }

        container.Shuffle();
    }
    
    public void RestartGame()
    {
        _engine.Stop(); 

        _grid.Clear(); 

        if (currentPiece != null)
        {
            _engine.RemoveComponent(currentPiece);
            _renderer.UnregisterDrawable(currentPiece);
            currentPiece.OnLockDown -= OnLockDown;
            currentPiece = null;
        }

        _engine.Start(); 
        SpawnNextPiece();
    }
}