using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlacers.Contracts
{
    public abstract class PackPlacer : MonoBehaviour, IPackPlacer
    {
        public abstract void Place(Block[][] blocks);
    }
}