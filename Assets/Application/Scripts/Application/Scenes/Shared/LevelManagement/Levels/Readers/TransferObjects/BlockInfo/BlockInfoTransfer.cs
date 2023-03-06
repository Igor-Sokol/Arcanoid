using Newtonsoft.Json;

namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.TransferObjects.BlockInfo
{
    public struct BlockInfoTransfer
    {
        [JsonProperty("tiles")]
        public Block[] Blocks;
    }
}