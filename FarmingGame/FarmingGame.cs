using FarmingGame.Engine.IO.Resources;
using FarmingGame.Engine.Scene;
using FarmingGame.Engine.Service;
using FarmingGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FarmingGame;

public class FarmingGame : Microsoft.Xna.Framework.Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SceneManager _sceneManager;
    
    public FarmingGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        // TODO: add screen size there -> it could be dynamic
        
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        // registering all classes with Service attribute
        ServiceManager.RegisterServices();
        
        ResourceManager.Instance.SetContentManager(Content);
        
        _sceneManager = new SceneManager();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        // setup scenes
        _sceneManager.AddScene(new MenuScene());
        _sceneManager.AddScene(new FarmScene());
        _sceneManager.SwitchScene("farm");
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _sceneManager.CurrentScene.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Firebrick);
        
         _sceneManager.CurrentScene.Draw(gameTime, _spriteBatch);

        base.Draw(gameTime);
    }
}