using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.Level.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "LevelManagement/Level")]
    public class ScriptableLevelInfo : ScriptableObject
    {
        [SerializeField] private LevelInfo levelInfo;

        public LevelInfo LevelInfo => levelInfo;
    }
}