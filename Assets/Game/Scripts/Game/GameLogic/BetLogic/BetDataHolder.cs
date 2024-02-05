using System.Collections.Generic;
using Game.Scripts.Game.GameLogic.GameData;

namespace Game.Scripts.Game.GameLogic.BetLogic
{
    public class BetDataHolder
    {
        private List<NumberData> _numbers;
        private List<ColorData> _colorsData;

        public List<NumberData> Numbers => _numbers;
        public List<ColorData> Colors => _colorsData;

        public void Reset()
        {
            _numbers = new List<NumberData>();
            _colorsData = new List<ColorData>();
        }

        public void AddNumber(NumberData numberData) => 
            _numbers.Add(numberData);

        public void AddColor(ColorData colorData) => 
            _colorsData.Add(colorData);
    }
}