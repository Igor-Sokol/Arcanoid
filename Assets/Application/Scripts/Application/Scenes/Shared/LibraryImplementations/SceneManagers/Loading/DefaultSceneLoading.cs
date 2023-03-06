using Application.Scripts.Library.SceneManagers.Contracts.Loading;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.SceneManagers.Loading
{
    public class DefaultSceneLoading : SceneLoading
    {
        [SerializeField] private Canvas canvas;
        
        public override YieldInstruction Enable()
        {
            canvas.gameObject.SetActive(true);
            return null;
        }

        public override YieldInstruction Disable()
        {
            canvas.gameObject.SetActive(false);
            return null;
        }
    }
}