using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class TmpTextValueUpdater : MonoBehaviour
    {
        [SerializeField] private List<TMP_Text> _valueList;

        public void UpdateValue(string txtValue)
        {
            UpdateTxt(txtValue);
        }

        public void UpdateValue(int value)
        {
            UpdateTxt(value+"");
        }

        private void UpdateTxt(string txtValue)
        {
            foreach (var someValue in _valueList)
            {
                someValue.text = txtValue;
            }
        }
    }
}
