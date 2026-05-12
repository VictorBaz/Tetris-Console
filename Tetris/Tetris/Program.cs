using Tetris.Engine;
using Tetris.Grid;
using Tetris.Manager;


Engine engine  = new Engine();

GameGrid grid = new GameGrid();

GridRenderer renderer = new GridRenderer(grid);

GameManager gameManager = new GameManager(engine, grid, renderer);

engine.AddComponent(renderer);
engine.AddComponent(gameManager);

engine.Run();