using System;
using System.Collections.Generic;
using FarmingGame.Engine.Service;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FarmingGame.Engine.IO.Resources;

// global singleton to manage all textures
// maybe I can just make separate ContentManager for
// every scene? It would make cleaning resources a lot easier

// TODO: Optimize if i find better solution

[Service]
public class ResourceManager
{
    private static readonly ResourceManager _instance = new ResourceManager();
    
    public Dictionary<string, Texture2D> Textures { get; set; }
    private ContentManager Content { get; set; }

    public static ResourceManager Instance => _instance;
    
    private ResourceManager()
    {
        Textures = new Dictionary<string, Texture2D>();
    }

    public void SetContentManager(ContentManager contentManager)
    {
        Content = contentManager;
    }

    public Texture2D Load2D(string assetName)
    {
        return Content.Load<Texture2D>(assetName);
    }

    public void Test()
    {
        Console.WriteLine("Testing service");
    }
}