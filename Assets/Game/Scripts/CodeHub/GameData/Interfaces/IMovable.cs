using UnityEngine;

namespace CodeHub.GameData
{
    public interface IMovable
    {
        public Vector2Int Direction { get; }
        public Vector2Int Position { get; }

        public void SetPosition(Vector2Int position);
        public void SetDirection(Vector2Int direction);
    }
}