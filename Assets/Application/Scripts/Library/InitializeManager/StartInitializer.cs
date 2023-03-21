using UnityEngine;

namespace Application.Scripts.Library.InitializeManager
{
    public class StartInitializer : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour initializer;

        private void Start()
        {
            (initializer as Initializer)?.Initialize();
        }
    }
}