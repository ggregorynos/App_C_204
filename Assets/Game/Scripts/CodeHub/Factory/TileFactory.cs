using System;
using CodeHub.Factory;
using CodeHub.GameData;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CodeHub.Factory
{
    public class TileFactory : ITileFactory
    {
        private TileBase landTile;
        private TileBase waterTile;
        private TileBase playerTile;
        private TileBase trackTile;
        private TileBase landEnemyTile;
        private TileBase waterEnemyTile;
        
        public void Load()
        {
            landTile = Resources.Load("Tiles/LandTile") as TileBase;
            waterTile = Resources.Load("Tiles/WaterTile") as TileBase;
            playerTile = Resources.Load("Tiles/PlayerTile") as TileBase;
            trackTile = Resources.Load("Tiles/TrackTile") as TileBase;
            landEnemyTile = Resources.Load("Tiles/LandEnemyTile") as TileBase;
            waterEnemyTile = Resources.Load("Tiles/WaterEnemyTile") as TileBase;
        }

        public TileBase Get(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Land:
                    return landTile;
                case TileType.Water:
                    return waterTile;
                case TileType.Trace:
                    return trackTile;
                case TileType.Player:
                    return playerTile;
                case TileType.LandEnemy:
                    return landEnemyTile;
                case TileType.WaterEnemy:
                    return waterEnemyTile;
                case TileType.FloodFillSmall:
                    return landTile;
                case TileType.Border:
                    return landTile;
                case TileType.FloodFillBig:
                    return landTile;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null);
            }
        }
    }
}