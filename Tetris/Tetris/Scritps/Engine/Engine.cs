using System.Diagnostics;
using Tetris.Component;

namespace Tetris.Engine;

public class Engine
{
    private readonly List<IComponent> _components = new(64);
    private readonly List<IComponent> _toAdd = new(16);
    private readonly List<int> _toRemoveIndices = new(16);

    private readonly List<IComponent> _updateList = new(64);

    public void AddComponent(IComponent component) => _toAdd.Add(component);
    
    public void RemoveComponent(IComponent component)
    {
        int index = _components.IndexOf(component);
        if (index != -1) _toRemoveIndices.Add(index);
    }

    private bool running = false;

    public void Run()
    {
        Start();
        Stopwatch stopwatch = new();
        stopwatch.Start(); 
    
        while (true)
        {
            if (!running) return;
            
            SyncComponents();
        
            double deltaTime = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Restart();
        
            _updateList.Clear();
            _updateList.AddRange(_components);

            foreach (var component in _updateList) 
            {
                component.Update(deltaTime);
            }
        
            Thread.Sleep(10);
        }
    }
    
    private void SyncComponents()
    {
        if (_toRemoveIndices.Count > 0)
        {
            _toRemoveIndices.Sort(); 
            for (int i = _toRemoveIndices.Count - 1; i >= 0; i--)
            {
                int index = _toRemoveIndices[i];
                _components.RemoveAt(index);
            }
            _toRemoveIndices.Clear();
        }

        if (_toAdd.Count > 0)
        {
            int i = 0;
            
            while (i < _toAdd.Count) 
            {
                var c = _toAdd[i];
                _components.Add(c);
                c.Start();
                i++;
            }
            _toAdd.Clear(); 
        }
    }

    public void Start() => running = true;

    public void Stop() => running = false;
}