using UnityEngine;

namespace CodeHub.WheelSpinLogic
{
    public class WheelSector: MonoBehaviour
    {
        [SerializeField] private WheelSectorData _wheelSectorData;
        [SerializeField] private int _value;

        public WheelSectorData WheelSectorData => _wheelSectorData;
        public int Value => _value;

        public void SetWheelSectorData(WheelSectorData wheelSectorData)
        {
            _wheelSectorData = wheelSectorData;
        }

        public void SetValue(int value)
        {
            _value = value;
        }
    }
}