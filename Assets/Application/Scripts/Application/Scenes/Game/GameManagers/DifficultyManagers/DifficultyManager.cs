using Application.Scripts.Application.Scenes.Game.GameManagers.DifficultyManagers.Configs;
using Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.DifficultyManagers
{
    public class DifficultyManager : MonoBehaviour, IReusable
    {
        [SerializeField] private DifficultyConfig difficultyConfig;
        [SerializeField] private BlockProgressManager progressManager;
        [SerializeField] private DifficultyTimeScale difficultyTimeScale;

        public void PrepareReuse()
        {
            difficultyTimeScale.Scale = difficultyConfig.BallSpeedScale.x;
        }
        
        private void OnEnable()
        {
            progressManager.OnBlockBroken += UpdateDifficulty;
        }

        private void OnDisable()
        {
            progressManager.OnBlockBroken -= UpdateDifficulty;
        }

        private void UpdateDifficulty()
        {
            difficultyTimeScale.Scale = Mathf.Lerp(difficultyConfig.BallSpeedScale.x, difficultyConfig.BallSpeedScale.y,
                progressManager.BrokenBlocks / (progressManager.TotalBlocks - 1f));
        }
    }
}