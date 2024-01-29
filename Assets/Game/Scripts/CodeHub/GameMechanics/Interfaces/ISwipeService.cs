using System;
using UnityEngine;

namespace CodeHub.GameMechanics
{
    public interface ISwipeService
    {
        public bool CanSwipe { get; }
        public Action<Vector2Int> OnSwipe { get; set; }

        public void EnableSwipe(bool enable);
    }
}