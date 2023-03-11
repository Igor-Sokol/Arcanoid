using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlacers.Contracts
{
    public abstract class PackPlacer : MonoBehaviour, IPackPlacer
    {
        public abstract Vector3[][] Place(Block[][] blocks);
    }
}