using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb.BombWayProviders.Contracts;
using Application.Scripts.Library.ChainFinders;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb.BombWayProviders.Implementations
{
    public class LineBombWay : BombWay
    {
        [SerializeField] private Vector2Int[] directions;
        [SerializeField] private int range;
        
        public override IEnumerable<IEnumerable<Vector2Int>> GetIndexes(Block[][] blocks, Vector2Int startIndex, IEnumerable<string> ignoreKeys)
        {
            foreach (var direction in directions)
            {
                yield return ChainFinder.GetChain(blocks, startIndex, 
                    new[] { new Vector2Int(direction.x, -direction.y) }, range, b =>
                    {
                        if (b)
                        {
                            return !ignoreKeys.Contains(b.Key);
                        }
                        return false;
                    })?.Skip(1);
            }
        }
    }
}