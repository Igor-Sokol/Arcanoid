using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb.BombWayProviders.Contracts
{
    public abstract class BombWay : MonoBehaviour
    {
        public abstract IEnumerable<IEnumerable<Vector2>> GetIndexes(Block[][] blocks, Vector2 startIndex, IEnumerable<string> ignoreKeys);
    }
}