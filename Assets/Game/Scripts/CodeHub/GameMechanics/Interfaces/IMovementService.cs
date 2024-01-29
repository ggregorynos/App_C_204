using CodeHub.GameData;

namespace CodeHub.GameMechanics
{
    public interface IMovementService
    {
        public void MoveObject(IMovable objMovable, ITileField tileField);
        public void MoveObjectWithReflection(IMovable objMovable, TileType borderTile, ITileField tileField);
    }
}