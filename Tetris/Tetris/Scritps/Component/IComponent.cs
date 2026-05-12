namespace Tetris.Component;

public interface IComponent
{
    /// <summary>
    /// Call beginning of Engine
    /// </summary>
    public virtual void Start(){}
    
    /// <summary>
    /// Call each frame
    /// </summary>
    /// <param name="deltaTime"></param>
    public virtual void Update(double deltaTime){}
}