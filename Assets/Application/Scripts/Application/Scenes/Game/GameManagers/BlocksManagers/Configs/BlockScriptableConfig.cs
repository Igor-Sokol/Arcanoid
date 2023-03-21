using Application.Scripts.Application.Scenes.Game.Units.Blocks.Configs;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Configs
{
    [CreateAssetMenu(fileName = "BlockConfig", menuName = "Block/Config")]
    public class BlockScriptableConfig : ScriptableObject
    {
        [SerializeField] private string configKey;
        [SerializeField] private BlockConfig config;

        public string ConfigKey => configKey;
        public BlockConfig Config => config;
    }
}