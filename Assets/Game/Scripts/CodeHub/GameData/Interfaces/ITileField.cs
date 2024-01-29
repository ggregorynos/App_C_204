using UnityEngine;

namespace CodeHub.GameData
{
    public interface ITileField: IField
    {
        public int Height { get; }
        public int Width { get; }
        public void SetTile(Vector2Int position, TileType tileType);
        public TileType GetTile(Vector2Int position);
    }
}