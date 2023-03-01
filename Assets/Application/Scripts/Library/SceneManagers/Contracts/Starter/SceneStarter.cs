using UnityEngine;

namespace Application.Scripts.Library.SceneManagers.Contracts.Starter
{
    public abstract class SceneStarter : MonoBehaviour, ISceneStarter
    {
        public abstract void StartScene();
    }
}