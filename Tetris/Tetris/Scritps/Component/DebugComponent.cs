namespace Tetris.Component;

public class DebugComponent : IComponent
{
    public void Update(double deltaTime)
    {
        Console.WriteLine(deltaTime);
    }
}