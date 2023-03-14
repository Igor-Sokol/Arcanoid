using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Application.Scripts.Application.Scenes.Game.Screen.UI.PlayerInputs
{
    public class PlayerInput : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private int _touchCount;
        
        public event Action<Vector2> OnTouching;
        public event Action<Vector2> OnStartTouching;
        public event Action<Vector2> OnStopTouching;
    
        public void OnDrag(PointerEventData eventData)
        {
            OnTouching?.Invoke(eventData.pointerCurrentRaycast.worldPosition);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _touchCount++;
            OnStartTouching?.Invoke(eventData.pointerCurrentRaycast.worldPosition);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _touchCount--;

            if (_touchCount <= 0)
            {
                OnStopTouching?.Invoke(eventData.pointerCurrentRaycast.worldPosition);
            }
        }
    }
}