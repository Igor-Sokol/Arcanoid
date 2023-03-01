using Application.Scripts.Application.Scenes.Game.GameInstallers.LevelInstallers.PackPlacers.Contracts;
using Application.Scripts.Application.Scenes.Game.Pools.BlockProvider.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Application.Scenes.Game.Units.Levels;
using Application.Scripts.Application.Scenes.Game.Units.Levels.Services.Readers.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.LevelInstallers
{
    public class LevelInstaller : MonoBehaviour
    {
        [SerializeField] private LevelReader levelReader;
        [SerializeField] private BlockProvider blockProvider;
        [SerializeField] private PackPlacer packPlacer;

        public void LoadLevel(LevelInfo levelInfo)
        {
            var blockKeys = levelReader.ReadPack(levelInfo);

            Block[][] blocks = new Block[blockKeys.Length][];

            for (int i = 0; i < blockKeys.Length; i++)
            {
                blocks[i] = new Block[blockKeys[i].Length];
                for (int j = 0; j < blockKeys[i].Length; j++)
                {
                    blocks[i][j] = blockProvider.GetBlock(blockKeys[i][j]);
                }
            }
            
            packPlacer.Place(blocks);
        }
    }
}