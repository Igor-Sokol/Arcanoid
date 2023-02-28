using UnityEngine;

namespace Application.Scripts.Library.DependencyInjection.Contracts
{
    public abstract class ServiceInstaller : MonoBehaviour, IServiceInstaller
    {
        public abstract ProjectContext ProjectContext { get; set; }
        public abstract void InstallService();
    }
}