using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.ProgressObjects;

namespace Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Repository.Contracts
{
    public interface IPackProgressRepository
    {
        void Save(PackProgressTransfer packProgressTransfer);
        PackProgressTransfer Load();
    }
}