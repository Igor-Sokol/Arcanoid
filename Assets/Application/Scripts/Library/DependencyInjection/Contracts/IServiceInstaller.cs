namespace Application.Scripts.Library.DependencyInjection.Contracts
{
    public interface IServiceInstaller
    {
        ProjectContext ProjectContext { get; set; }
        void InstallService();
    }
}