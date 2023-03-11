using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlacers.Contracts
{
    public interface IPackPlacer
    {
        Vector3[][] Place(Block[][] blocks);
    }
}