using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb.BombWayProviders.Contracts;
using Application.Scripts.Library.ChainFinders;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb.BombWayProviders.Implementations
{
    public class ChainBombWay : BombWay
    {
        [SerializeField] private Vector2[] moveRules;
        
        public override IEnumerable<IEnumerable<Vector2>> GetIndexes(Block[][] blocks, Vector2 startIndex, IEnumerable<string> ignoreKeys)
        {
            yield return ChainFinder.GetLongestChain(blocks, b => b.Key, (b) => b && !ignoreKeys.Contains(b.Key),
                startIndex, moveRules)?.Skip(1);
        }
    }
}