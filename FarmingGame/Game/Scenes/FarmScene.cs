using System;
using System.IO;
using FarmingGame.Engine.IO.Resources;
using FarmingGame.Engine.Scene;
using FarmingGame.Game.Config;
using FarmingGame.Game.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FarmingGame.Game.Scenes;

public class FarmScene : IScene
{
    // I start to think that if i use variables IScene should be regular class not interface
    public string Name { get; } = "farm";
    // provider by services container
    private ResourceManager _resourceManager;
    private Texture2D TileMap { get; set; } 
    private Tile[,] Tiles { get; set; }
    
    private Camera Camera;
    private float previousScrollValue = Mouse.GetState().ScrollWheelValue;

    private float dayNightCycleTime = 0f;
    private float cycleDuration = 10f;
    private Color currentTint = Color.White;
    
    public void Init()
    {
        // loading tile map for overworld
        _resourceManager = ResourceManager.Instance;
        TileMap = _resourceManager.Load2D("overworld");
        
        // Getting tiles types for overworld
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Content", "Config", "tiles.yml");
        TileMapper tileMapper = new TileMapper(TileMap, path);
        GameState.Instance.TileTypes = tileMapper.Map();

        int size = GameState.Instance.OverWorldSize;
        Tiles = new Tile[size, size];
        ProceduralGenerateTiles();

        Camera = new Camera();
    }
    
    private void ProceduralGenerateTiles()
    {
        // replace with better algorithm xD
        var random = new Random();

        // in foreach i cannot modify objects (it gives you local reference)
        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                var randType = random.Next(1, GameState.Instance.TileTypes.Length + 1);    
                // for real these positions are stupid
                Tiles[i, j] = new Tile(GameState.Instance.TileTypes[randType - 1], i, j);
            }
        }
    }
    
    public void Update(GameTime gameTime)
    {
        // check is R key is press
        // it will recreated map 
        KeyboardState newState = Keyboard.GetState();
        if (newState.IsKeyDown(Keys.R))
        {
            Console.WriteLine("R is pressed!");
        }
        
        // handling camera move
        float cameraSpeed = 500f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        Vector2 position = Camera.Position;
        
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
            position.Y -= cameraSpeed;
        if (Keyboard.GetState().IsKeyDown(Keys.Down))
            position.Y += cameraSpeed;
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
            position.X -= cameraSpeed;
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
            position.X += cameraSpeed;

        Camera.Position = position;
        
        // handling camera zooming
        if (Mouse.GetState().ScrollWheelValue > previousScrollValue)
        {
            Camera.Zoom += 0.1f;
        }
        else if (Mouse.GetState().ScrollWheelValue < previousScrollValue)
        {
            Camera.Zoom -= 0.1f;
        }

        previousScrollValue = Mouse.GetState().ScrollWheelValue;
        
        // day night cycle
        dayNightCycleTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (dayNightCycleTime > cycleDuration)
        {
            dayNightCycleTime = 0f;
        }
        
        float cycleProgress = dayNightCycleTime / cycleDuration;
        float brightness = 0.6f + (1.0f - 0.6f) * (float)Math.Sin(cycleProgress * MathHelper.TwoPi);
        currentTint = new Color(brightness, brightness, brightness);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Camera.GetViewMatrix());
        
        int scaledTileSize = GameState.Instance.ScaledTileSize;
        foreach (var tile in Tiles)
        {
            spriteBatch.Draw(
                TileMap,
                new Rectangle(
                    tile.PositionX * scaledTileSize,
                    tile.PositionY * scaledTileSize,
                    scaledTileSize, scaledTileSize),
                tile.TileType.Texture,
                //Color.White
                currentTint
            );   
        }
        
        spriteBatch.End();
    }
}