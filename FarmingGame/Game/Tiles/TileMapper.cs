using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FarmingGame.Game.Tiles;

public class TileMapper
{
    private readonly IDeserializer _deserializer;
    public Texture2D TileMap { get; set; }
    public string Name { get; set; }

    public TileMapper(Texture2D map, string fileName)
    {
        TileMap = map;
        Name = fileName;
        
        _deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
    }

    // this method should be more general
    // and not work only with overworld tiles
    public TileType[] Map()
    { 
        // "Config/tiles.yml"
        var yamlContent = File.ReadAllText(Name);
        var rawTiles = _deserializer
            .Deserialize<Dictionary<string, Dictionary<string, List<Dictionary<string, object>>>>>(yamlContent);
        var overworldTiles = rawTiles["tiles"]["overworld"];
        var tilesTypes = new TileType[overworldTiles.Count];

        // custom deserialization to map x and y into Rectangle object
        foreach (var tile in overworldTiles)
        {
            int id = Convert.ToInt32(tile["id"]);
            string name = tile["name"].ToString();
            int x = Convert.ToInt32(tile["x"]);
            int y = Convert.ToInt32(tile["y"]);
            bool walkable = Convert.ToBoolean(tile["walkable"]);

            var tileType = new TileType(id, name, x, y, walkable);
            tilesTypes[id-1] = tileType;
        }

        foreach (var tile in tilesTypes)
        {
            Console.WriteLine($"{tile.Name} {tile.Id}");
        }

        return tilesTypes;
    }
    
}