using FarmingGame.Game.Tiles;

namespace FarmingGame.Game.Config;

// for now i made singleton for storing global data, i will change it to have more
// elastic solution later
public class GameState
{
    private static GameState _instance;
    
    public static GameState Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameState();
                _instance.ScaledTileSize = _instance.BaseTileSize * _instance.Scale;
            }
            return _instance;
        }
    }
    
    public readonly int BaseTileSize = 32;
    public int Scale = 3;
    public int ScaledTileSize;
    public int OverWorldSize = 10;

    public TileType[] TileTypes { get; set; }
    
    // preventing creating another instances
    private GameState(){}
}