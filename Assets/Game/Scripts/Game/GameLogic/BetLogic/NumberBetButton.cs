using Game.Scripts.Game.GameLogic.GameData;
using UnityEngine;

namespace Game.Scripts.Game.GameLogic.BetLogic
{
    public class NumberBetButton : BaseBetButton
    {
        [field: SerializeField] public NumberData NumberData;
    }
}