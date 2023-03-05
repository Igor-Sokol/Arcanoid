using Application.Scripts.Library.InitializeManager.Contracts;

namespace Application.Scripts.Library.InitializeManager
{
    public class StartInitializer : Initializer
    {
        private IInitializing[] _initializing;

        private void Start()
        {
            Initialize();
        }
    }
}