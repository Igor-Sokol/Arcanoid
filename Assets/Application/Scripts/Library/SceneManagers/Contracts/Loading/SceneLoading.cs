using UnityEngine;

namespace Application.Scripts.Library.SceneManagers.Contracts.Loading
{
    public abstract class SceneLoading : MonoBehaviour, ISceneLoading
    {
        public abstract YieldInstruction Enable();
        public abstract YieldInstruction Disable();
    }
}