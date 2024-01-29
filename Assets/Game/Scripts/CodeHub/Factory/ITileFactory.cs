using CodeHub.GameData;
using UnityEngine.Tilemaps;

namespace CodeHub.Factory
{
    public interface ITileFactory
    {
        public void Load();
        public TileBase Get(TileType tileType);
    }
}