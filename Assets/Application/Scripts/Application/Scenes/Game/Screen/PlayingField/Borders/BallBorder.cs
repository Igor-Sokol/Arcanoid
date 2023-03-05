using System.Collections.Generic;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.ScreenInfo;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PlayingField.Borders
{
    public class BallBorder : MonoBehaviour, IInitializing
    {
        [SerializeField] private ScreenInfo screenInfo;
        [SerializeField] private EdgeCollider2D edgeCollider2d;
        [SerializeField] private Vector2[] percentagePositions;
        
        public void Initialize()
        {
            List<Vector2> points = new List<Vector2>();

            points.Clear();
            foreach (var position in percentagePositions)
            {
                points.Add(screenInfo.PositionFromPercentage(position));
            }

            edgeCollider2d.SetPoints(points);
        }
    }
}