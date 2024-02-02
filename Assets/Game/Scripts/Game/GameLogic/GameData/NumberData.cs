using UnityEngine;

namespace Game.Scripts.Game.GameLogic.GameData
{
    [CreateAssetMenu(fileName = "NumberData", menuName = "NumberData", order = 1)]
    public class NumberData : ScriptableObject
    {
        [field: SerializeField] public ColorData ColorData;
        [field: SerializeField] public int Number;
    }
}