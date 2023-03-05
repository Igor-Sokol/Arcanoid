using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Level.Readers.Contracts;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.Level.Readers.Implementations.Json
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

            JObject file = JObject.Parse(levelInfo.text);

            int height = file["height"].Value<int>();
            int width = file["width"].Value<int>();
            
            int[] data = file["data"].Select(d => d.Value<int>()).ToArray();

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
            
            JObject file = JObject.Parse(blocksInfo.text);
            JToken blocks = file["tiles"];
            
            foreach (var block in blocks)
            {
                int id = block["id"].Value<int>();

                foreach (var blockKeyProperty in block["properties"].Where(p => p["name"].Value<string>() == "BlockKey"))
                {
                    _blocks.Add(++id, blockKeyProperty["value"].Value<string>());
                }
            }
            
            Resources.UnloadAsset(blocksInfo);
        }
    }
}