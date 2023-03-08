namespace Application.Scripts.Application.Scenes.Shared.Energy.Repository.Contracts
{
    public interface IEnergyRepository
    {
        void Save(EnergySaveObject energySave);
        EnergySaveObject Load();
    }
}