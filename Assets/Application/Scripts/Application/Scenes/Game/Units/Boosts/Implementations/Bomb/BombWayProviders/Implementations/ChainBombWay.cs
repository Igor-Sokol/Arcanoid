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
        [SerializeField] private Vector2Int[] moveRules;
        
        public override IEnumerable<IEnumerable<Vector2Int>> GetIndexes(Block[][] blocks, Vector2Int startIndex, IEnumerable<string> ignoreKeys)
        {
            yield return ChainFinder.GetLongestChain(blocks, b => b.Key, (b) => b && !ignoreKeys.Contains(b.Key),
                startIndex, moveRules)?.Skip(1);
        }
    }
}