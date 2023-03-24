using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.ScreenInfo;
using Sirenix.Utilities;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PlayingField.Effectors
{
    public class EffectorManager : MonoBehaviour, IInitializing
    {
        [SerializeField] private ScreenInfo screenInfo;
        [SerializeField] private Effector[] effectors;
        
        public void Initialize()
        {
            foreach (var effector in effectors)
            {
                effector.transform.position = screenInfo.PositionFromPercentage(effector.PositionPercentage);
            }
        }

        public void Play()
        {
            effectors.ForEach(e => e.Play());
        }

        public void Stop()
        {
            effectors.ForEach(e => e.Stop());
        }
    }
}