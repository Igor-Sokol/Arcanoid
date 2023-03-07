using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.Contracts;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.TransferObjects.BlockInfo;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.TransferObjects.Level;
using Newtonsoft.Json;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.Implementations.Json
{
    public class JsonLevelReader : LevelReader
    {
        private readonly Dictionary<int, string> _blocks = new Dictionary<int, string>();

        [SerializeField] private string blocksInfoPath;
        
        public override string[][] ReadPack(LevelInfo level)
        {
            ReadBlocksInfo();
            return ReadLevel(level);
        }

        private string[][] ReadLevel(LevelInfo level)
        {
            TextAsset levelInfo = Resources.Load<TextAsset>(level.Path);

            var levelTransferObject = JsonConvert.DeserializeObject<LevelTransferObject>(levelInfo.text);

            int height = levelTransferObject.Height;
            int width = levelTransferObject.Width;

            int[] data = levelTransferObject.Data;

            string[][] blockKeys = new string[height][];
            for (int i = 0, index = 0; i < height; i++)
            {
                blockKeys[i] = new string[width];
                for (int j = 0; j < width; j++, index++)
                {
                    if (_blocks.TryGetValue(data[index], out var value))
                    {
                        blockKeys[i][j] = value;
                    }
                    else
                    {
                        blockKeys[i][j] = string.Empty;
                    }
                }
            }
            
            Resources.UnloadAsset(levelInfo);

            return blockKeys;
        }
        
        private void ReadBlocksInfo()
        {
            TextAsset blocksInfo = Resources.Load<TextAsset>(blocksInfoPath);

            BlockInfoTransfer block = JsonConvert.DeserializeObject<BlockInfoTransfer>(blocksInfo.text);

            _blocks.Clear();
            foreach (var tile in block.Blocks)
            {
                _blocks.Add(tile.Id + 1, tile.Properties.FirstOrDefault(p => p.Name == "BlockKey").Value);
            }

            Resources.UnloadAsset(blocksInfo);
        }
    }
}