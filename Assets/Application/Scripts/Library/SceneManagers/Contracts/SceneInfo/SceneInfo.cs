using UnityEngine;

namespace Application.Scripts.Library.SceneManagers.Contracts.SceneInfo
{
    public abstract class SceneInfo : MonoBehaviour
    {
        public abstract string SceneName { get; }
    }
}