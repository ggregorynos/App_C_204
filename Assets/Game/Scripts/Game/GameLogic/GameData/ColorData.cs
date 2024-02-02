using UnityEngine;

namespace Game.Scripts.Game.GameLogic.GameData
{
    [CreateAssetMenu(fileName = "ColorData", menuName = "ColorData", order = 1)]
    public class ColorData : ScriptableObject
    {
        [field: SerializeField] public Sprite BackPanel;
    }
}
