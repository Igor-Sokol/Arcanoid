using UnityEngine;

namespace Application.Scripts.Library.ScreenInfo
{
    public class ScreenInfo : MonoBehaviour
    {
        [SerializeField] private Camera workingCamera;
        
        public Vector2 ScreenSize { get; private set; }
        public Vector2 HalfScreenSize { get; private set; }
        public Vector2 ScreenLeftBottom { get; private set; }
        public Vector2 ScreenRightUpper { get; private set; }
        public Vector2 Resolution { get; private set; }

        private void Awake()
        {
            UpdateFieldSize();
        }

        public Vector2 PositionFromPercentage(Vector2 percentage)
        {
            var position = new Vector2(ScreenSize.x * percentage.x - HalfScreenSize.x,
                ScreenSize.y * percentage.y - HalfScreenSize.y);

            return position;
        }

        public Vector2 WorldToScreenPoint(Vector3 position)
        {
            return workingCamera.WorldToScreenPoint(position);
        }
        
        public Vector2 ScreenToWorldPoint(Vector3 position)
        {
            return workingCamera.ScreenToWorldPoint(position);
        }
        
        private void UpdateFieldSize()
        {
            Resolution = new Vector2(Screen.width, Screen.height);
            
            ScreenLeftBottom = workingCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
            ScreenRightUpper = workingCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            ScreenSize = new Vector2(Mathf.Abs(ScreenLeftBottom.x) + Mathf.Abs(ScreenRightUpper.x),
                Mathf.Abs(ScreenLeftBottom.y) + Mathf.Abs(ScreenRightUpper.y));
            
            HalfScreenSize = ScreenSize / 2;
        }
    }
}