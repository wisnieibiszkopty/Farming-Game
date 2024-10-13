using System.Diagnostics;
using FarmingGame.Engine;

namespace FarmingGame.Game.Tiles;

public class Tile : IEntity
{
    public TileType TileType { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    
    public IEntity ContainsItem { get; set; }

    public Tile(TileType type, int x, int y)
    {
        TileType = type;
        PositionX = x;
        PositionY = y;
    }
    
    public void Update()
    {
        Debug.WriteLine("Update in tile");
    }
    
}