using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.ProgressObjects;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Repository.Contracts
{
    public abstract class PackProgressRepository : MonoBehaviour, IPackProgressRepository
    {
        public abstract void Save(PackProgressTransfer packProgressTransfer);
        public abstract PackProgressTransfer Load();
    }
}