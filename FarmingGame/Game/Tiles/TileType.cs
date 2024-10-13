using FarmingGame.Game.Config;
using Microsoft.Xna.Framework;

namespace FarmingGame.Game.Tiles;

public class TileType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Rectangle Texture { get; set; }
    public bool Walkable { get; set; }

    public TileType(int id, string name, int x, int y, bool walkable)
    {
        Id = id;
        Name = name;
        Walkable = walkable;

        int baseSize = GameState.Instance.BaseTileSize;
        Texture = new Rectangle(baseSize * x, baseSize * y, baseSize, baseSize);
    }
}