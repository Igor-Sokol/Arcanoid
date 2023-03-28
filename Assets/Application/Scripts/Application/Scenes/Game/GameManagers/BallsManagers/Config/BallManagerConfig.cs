using Application.Scripts.Application.Scenes.Game.Units.Balls;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Config
{
    [CreateAssetMenu(fileName = "BallManager", menuName = "BallManager/Config")]
    public class BallManagerConfig : ScriptableObject, IBallManagerConfig
    {
        [SerializeField] private Ball ballKey;

        public Ball BallKey => ballKey;
    }
}