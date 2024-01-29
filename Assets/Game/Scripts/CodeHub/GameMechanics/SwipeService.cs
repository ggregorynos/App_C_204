using System;
using UnityEngine;

namespace CodeHub.GameMechanics
{
    public class SwipeService : MonoBehaviour, ISwipeService
    {
        public  Action<Vector2Int> OnSwipe { get; set; }
        private Vector2 tapPosition;
        private Vector2 swipeDelta;
        private float deadZone = 50;
        private bool isSwiping;
        
        public bool CanSwipe { get; private set; }

        public void EnableSwipe(bool enable)
        {
            CanSwipe = enable;
        }


        public void Update()
        {
            if(!CanSwipe)
                return;
            
            if (!Application.isMobilePlatform)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isSwiping = true;
                    tapPosition = Input.mousePosition;
                }
                else if(Input.GetMouseButtonUp(0))
                {
                    ResetSwipe();
                }
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        isSwiping = true;
                        tapPosition = Input.GetTouch(0).position;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Canceled ||
                             Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        ResetSwipe();
                    }

                }
            }

            CheckSwipe();
        }

        private void CheckSwipe()
        {
            swipeDelta = Vector2.zero;

            if (isSwiping)
            {
                if(Application.isMobilePlatform==false&& Input.GetMouseButton(0))
                {
                    swipeDelta = (Vector2) Input.mousePosition - tapPosition;
                }
                else if (Input.touchCount > 0)
                {
                    swipeDelta = Input.GetTouch(0).position - tapPosition;
                }
            }

            if (!(swipeDelta.magnitude > deadZone)) return;
            
            if (Math.Abs(swipeDelta.x) > Math.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    Debug.Log("OnRightSwipe");
                    OnSwipe?.Invoke(Vector2Int.right);
                }
                else
                {
                    Debug.Log("OnLeftSwipe");
                    OnSwipe?.Invoke(Vector2Int.left);
                }
            }
            else
            {
                if (swipeDelta.y > 0)
                {
                    Debug.Log("OnUpSwipe");
                    OnSwipe?.Invoke(Vector2Int.up);
                }
                else
                {
                    Debug.Log("OnDownSwipe");
                    OnSwipe?.Invoke(Vector2Int.down);
                }
            }
                
            ResetSwipe();
        }

        private void ResetSwipe()
        {
            isSwiping = false;
            tapPosition = Vector2.zero;
            swipeDelta = Vector2.zero;
        }
    }
}