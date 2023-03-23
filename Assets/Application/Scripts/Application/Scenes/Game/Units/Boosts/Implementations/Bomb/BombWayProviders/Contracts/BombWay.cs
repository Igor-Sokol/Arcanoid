using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb.BombWayProviders.Contracts
{
    public abstract class BombWay : MonoBehaviour
    {
        public abstract IEnumerable<IEnumerable<Vector2Int>> GetIndexes(Block[][] blocks, Vector2Int startIndex, IEnumerable<string> ignoreKeys);
    }
}