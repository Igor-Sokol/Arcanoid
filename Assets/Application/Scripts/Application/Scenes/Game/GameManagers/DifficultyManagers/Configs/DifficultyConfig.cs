using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.DifficultyManagers.Configs
{
    [CreateAssetMenu(fileName = "Difficulty", menuName = "Difficulty/Config")]
    public class DifficultyConfig : ScriptableObject
    {
        [SerializeField] private Vector2 ballSpeedScales;

        public Vector2 BallSpeedScale => ballSpeedScales;
    }
}