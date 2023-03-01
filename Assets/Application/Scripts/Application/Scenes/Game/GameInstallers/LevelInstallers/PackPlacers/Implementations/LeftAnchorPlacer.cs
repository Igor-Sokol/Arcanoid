using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameInstallers.LevelInstallers.PackPlacers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Library.ScreenInfo;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.LevelInstallers.PackPlacers.Implementations
{
    public class LeftAnchorPlacer : PackPlacer
    {
        [SerializeField] private ScreenInfo screenInfo;
        [SerializeField] private Vector2 screenOffsetPercentage;
        [SerializeField] private Vector2 blockSpacePercentage;
        [SerializeField] private Vector2 fieldSpacePercentage;

        public override void Place(Block[][] blocks)
        {
            Vector2 screenOffset = new Vector2(screenInfo.ScreenSize.x * screenOffsetPercentage.x, screenInfo.ScreenSize.y * screenOffsetPercentage.y);
            Vector2 fieldSpaceOffset =
                new Vector2(screenInfo.ScreenSize.x * fieldSpacePercentage.x, screenInfo.ScreenSize.y * fieldSpacePercentage.y);
            Vector2 blockSpaceOffset =
                new Vector2(screenInfo.ScreenSize.x * blockSpacePercentage.x, screenInfo.ScreenSize.y * blockSpacePercentage.y);

            float maxBlocksInRow = blocks.Max(b => b.Length);
            float freeXSpace = screenInfo.ScreenSize.x - (fieldSpaceOffset.x * 2f) -
                               (blockSpaceOffset.x * maxBlocksInRow + blockSpaceOffset.x);
            float blockXSize = freeXSpace / maxBlocksInRow;
        
            Vector2 maxBlockSize = FindBiggestBlock(blocks).BlockRenderer.GetWordSize();
            Vector2 targetBlockSize = maxBlockSize * blockXSize / maxBlockSize.x;
        
            Vector2 positionAnchor = new Vector2(screenInfo.ScreenLeftBottom.x, screenInfo.ScreenRightUpper.y);
            positionAnchor += screenOffset;
            positionAnchor += fieldSpaceOffset;
            positionAnchor += new Vector2(targetBlockSize.x / 2f, -targetBlockSize.y / 2f);

            for (int row = 0; row < blocks.Length; row++)
            {
                Vector2 position = positionAnchor - new Vector2(0f, targetBlockSize.y * row);
                position += new Vector2(blockSpaceOffset.x, -blockSpaceOffset.y * row);
            
                for (int column = 0; column < blocks[row].Length; column++)
                {
                    Block block = blocks[row][column];
                    if (block)
                    {
                        var blockTransform = block.transform;
                        blockTransform.position = position;

                        Vector2 blockSize = block.BlockRenderer.GetWordSize();
                        Vector2 blockScale = targetBlockSize / blockSize;
                        float scale = blockScale.x < blockScale.y ? blockScale.x : blockScale.y;
                        blockTransform.localScale = new Vector3(scale, scale, scale);
                    }

                    position += new Vector2(targetBlockSize.x + blockSpaceOffset.x, 0);
                }
            }
        }

        private Block FindBiggestBlock(Block[][] blockTable)
        {
            (Block block, float value) biggest = (null, float.MinValue);

            foreach (var blocks in blockTable)
            {
                foreach (var temp in blocks)
                {
                    if (temp)
                    {
                        Vector2 blockSize = temp.BlockRenderer.GetWordSize();
                        float value = blockSize.x * blockSize.y;
                
                        if (biggest.value < value)
                        {
                            biggest = (temp, value);
                        }
                    }
                }
            }

            return biggest.block;
        }
    }
}
