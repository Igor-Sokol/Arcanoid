using System;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.Contracts
{
    [Serializable]
    public struct BlockInfo
    {
        [SerializeField] private string blockKey;
        [SerializeField] private string configKey;

        public string BlockKey => blockKey;
        public string ConfigKey => configKey;

        public BlockInfo(string blockKey, string configKey)
        {
            this.blockKey = blockKey;
            this.configKey = configKey;
        }
    }
}