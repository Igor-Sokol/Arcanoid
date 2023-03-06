using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Screen.UI.Header;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.ScreenInfo;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PlayingField.Borders
{
    public class BallBorder : MonoBehaviour, IInitializing
    {
        [SerializeField] private ScreenInfo screenInfo;
        [SerializeField] private HeaderSize headerSize;
        [SerializeField] private EdgeCollider2D edgeCollider2d;
        [SerializeField] private Vector2[] percentagePositions;
        
        public void Initialize()
        {
            List<Vector2> points = new List<Vector2>();

            points.Clear();
            foreach (var position in percentagePositions)
            {
                var offset = PositionFromPercentage(position);

                points.Add(offset);
            }

            edgeCollider2d.SetPoints(points);
        }
        
        private Vector2 PositionFromPercentage(Vector2 percentage)
        {
            var position = new Vector2(screenInfo.ScreenSize.x * percentage.x - screenInfo.HalfScreenSize.x,
                (screenInfo.ScreenSize.y - headerSize.Size.y) * percentage.y - screenInfo.HalfScreenSize.y);

            return position;
        }
    }
}