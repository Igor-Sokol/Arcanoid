using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlacers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Application.Scenes.Shared.UI.Header;
using Application.Scripts.Library.ScreenInfo;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlacers.Implementations
{
    public class LeftAnchorPlacer : PackPlacer
    {
        [SerializeField] private ScreenInfo screenInfo;
        [SerializeField] private HeaderSize headerSize;
        [SerializeField] private Vector2 screenOffsetPercentage;
        [SerializeField] private Vector2 blockSpace;
        [SerializeField] private Vector2 fieldSpacePercentage;

        public override Vector3[][] Place(Block[][] blocks)
        {
            Vector2 screenOffset = new Vector2(screenInfo.ScreenSize.x * screenOffsetPercentage.x,
                screenInfo.ScreenSize.y * screenOffsetPercentage.y);
            screenOffset -= new Vector2(0, headerSize.Size.y);

            Vector2 fieldSpaceOffset =
                new Vector2(screenInfo.ScreenSize.x * fieldSpacePercentage.x,
                    (screenInfo.ScreenSize.y - headerSize.Size.y) * fieldSpacePercentage.y);


            float maxBlocksInRow = blocks.Max(b => b.Length);
            float freeXSpace = screenInfo.ScreenSize.x - (fieldSpaceOffset.x * 2f) -
                               (blockSpace.x * maxBlocksInRow + blockSpace.x);
            float blockXSize = freeXSpace / maxBlocksInRow;
        
            Vector2 maxBlockSize = FindBiggestBlock(blocks).BlockView.Sprite.bounds.size;
            Vector2 targetBlockSize = maxBlockSize * blockXSize / maxBlockSize.x;
        
            Vector2 positionAnchor = new Vector2(screenInfo.ScreenLeftBottom.x, screenInfo.ScreenRightUpper.y);
            positionAnchor += screenOffset;
            positionAnchor += fieldSpaceOffset;
            positionAnchor += new Vector2(targetBlockSize.x / 2f, -targetBlockSize.y / 2f);

            Vector3[][] positions = new Vector3[blocks.Length][];
            for (int row = 0; row < blocks.Length; row++)
            {
                Vector2 position = positionAnchor - new Vector2(0f, targetBlockSize.y * row);
                position += new Vector2(blockSpace.x, -blockSpace.y * row);

                positions[row] = new Vector3[blocks[row].Length];
                for (int column = 0; column < blocks[row].Length; column++)
                {
                    Block block = blocks[row][column];
                    if (block)
                    {
                        var blockTransform = block.transform;
                        blockTransform.position = position;
                        positions[row][column] = position;
                        
                        Vector2 blockSize = block.BlockView.Sprite.bounds.size;
                        Vector2 blockScale = targetBlockSize / blockSize;
                        float scale = blockScale.x < blockScale.y ? blockScale.x : blockScale.y;
                        blockTransform.localScale = new Vector3(scale, scale, scale);
                    }

                    position += new Vector2(targetBlockSize.x + blockSpace.x, 0);
                }
            }

            return positions;
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
                        Vector2 blockSize = temp.BlockView.Sprite.bounds.size;
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
