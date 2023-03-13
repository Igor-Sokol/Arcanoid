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
        [SerializeField] private Vector2[] directions;
        [SerializeField] private int range;
        
        public override IEnumerable<IEnumerable<Vector2>> GetIndexes(Block[][] blocks, Vector2 startIndex, IEnumerable<string> ignoreKeys)
        {
            foreach (var direction in directions)
            {
                yield return ChainFinder.GetChain(blocks, startIndex, 
                    new[] { new Vector2(direction.x, -direction.y) }, range, b =>
                    {
                        if (b)
                        {
                            return !ignoreKeys.Contains(b.Key);
                        }
                        return false;
                    }).Skip(1);
            }
        }
    }
}