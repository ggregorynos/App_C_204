using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CodeHub.OtherUtilities
{
    public class SegmentsActivator : MonoBehaviour
    {
        [SerializeField] private List<Image> elements;
        [SerializeField] private Sprite activeElement;
        [SerializeField] private Sprite nonActiveElement;

        public int NumberElements => elements.Count;

        public void ActiveElements(int count)
        {
            ActiveAllElements(false);
            for (int i = 0; i < elements.Count; i++)
            {
                if(i<count)
                    ActiveElement(true,elements[i]);
            }
        }

        private void ActiveAllElements(bool active)
        {
            foreach (var element in elements)
            {
                ActiveElement(active, element);
            }
        }

        public int GetActiveCount()
        {
            return elements.Count(element => element.sprite == activeElement);
        }

        private void ActiveElement(bool active, Image element)
        {
            if (active)
                element.sprite = activeElement;
            else
                element.sprite = nonActiveElement;
        }
    }
}