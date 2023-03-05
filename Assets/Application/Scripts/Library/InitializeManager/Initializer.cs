using System.Linq;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

namespace Application.Scripts.Library.InitializeManager
{
    public class Initializer : MonoBehaviour, IInitializing
    {
        private IInitializing[] _initializing;
        
        [SerializeField] private MonoBehaviour[] initializing;
        
        public void Initialize()
        {
            _initializing = initializing.OfType<IInitializing>().ToArray();

            foreach (var item in _initializing)
            {
                item.Initialize();
            }
        }
    }
}