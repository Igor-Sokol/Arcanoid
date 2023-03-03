using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Application.Scripts.Application.Scenes.Game.PlayingField.PlayerInputs
{
    public class PlayerInput : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<Vector2> OnTouching;
        public event Action<Vector2> OnStartTouching;
        public event Action<Vector2> OnStopTouching;
    
        public void OnDrag(PointerEventData eventData)
        {
            OnTouching?.Invoke(eventData.pointerCurrentRaycast.worldPosition);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnStartTouching?.Invoke(eventData.pointerCurrentRaycast.worldPosition);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnStopTouching?.Invoke(eventData.pointerCurrentRaycast.worldPosition);
        }
    }
}