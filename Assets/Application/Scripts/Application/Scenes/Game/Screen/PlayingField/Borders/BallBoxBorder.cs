using Application.Scripts.Application.Scenes.Game.Screen.UI.Header;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.ScreenInfo;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PlayingField.Borders
{
    public class BallBoxBorder : MonoBehaviour, IInitializing
    {
        [SerializeField] private ScreenInfo screenInfo;
        [SerializeField] private HeaderSize headerSize;
        [SerializeField] private BoxCollider2D[] colliders;
        [SerializeField] private Vector2[] percentagePositions;
        [SerializeField] private Vector2[] percentageSize;
        [SerializeField] private Vector2[] percentageOffset;
        
        public void Initialize()
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var offset = PositionFromPercentage(percentagePositions[i]);
                colliders[i].size = Vector2.Scale(screenInfo.ScreenSize, percentageSize[i]);
                colliders[i].offset = offset + Vector2.Scale(colliders[i].size, percentageOffset[i]);
            }
        }
        
        private Vector2 PositionFromPercentage(Vector2 percentage)
        {
            var position = new Vector2(screenInfo.ScreenSize.x * percentage.x - screenInfo.HalfScreenSize.x,
                (screenInfo.ScreenSize.y - headerSize.Size.y) * percentage.y - screenInfo.HalfScreenSize.y);

            return position;
        }
    }
}