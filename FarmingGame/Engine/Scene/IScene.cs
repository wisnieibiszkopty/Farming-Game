using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FarmingGame.Engine.Scene;

public interface IScene
{
    string Name { get; }
    
    void Init();
    void Update(GameTime gameTime);
    void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}