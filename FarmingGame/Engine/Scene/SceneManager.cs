using System;
using System.Collections.Generic;
using System.Linq;
using FarmingGame.Game.Exceptions;

namespace FarmingGame.Engine.Scene;

public class SceneManager
{
    private List<IScene> Scenes;
    public IScene CurrentScene { get; set; }

    public SceneManager()
    {
        Scenes = new List<IScene>();
    }
    
    public void AddScene(IScene scene)
    {
        if (Scenes.Any(s => s.Name == scene.Name))
        {
            throw new InvalidOperationException($"Scene with name {scene.Name} already exists");
        }
        
        Scenes.Add(scene);
    }

    public void SwitchScene(String name)
    {
        var scene = Scenes.Find(s => s.Name == name);

        if (scene != null)
        {
            CurrentScene = scene;   
            CurrentScene.Init();
        }
        else
        {
            throw new SceneNotFoundException($"Scene with name {name} doesn't exists");   
        }
    }
}