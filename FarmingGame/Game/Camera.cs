using Microsoft.Xna.Framework;

namespace FarmingGame.Game;

public class Camera
{
    public Vector2 Position { get; set; }
    public float Zoom { get; set; }

    public Camera()
    {
        Position = Vector2.Zero;
        Zoom = 1.0f;
    }
    
    public Matrix GetViewMatrix()
    {
        return Matrix.CreateTranslation(new Vector3(-Position, 0)) *
               Matrix.CreateScale(Zoom);
    }
}