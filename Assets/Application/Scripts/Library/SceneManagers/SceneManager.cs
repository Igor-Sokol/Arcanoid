using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Library.SceneManagers.Contracts.Loading;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Application.Scripts.Library.SceneManagers
{
    public class SceneManager : SerializedMonoBehaviour
    {
        private AsyncOperation _sceneLoadingHandler;
        private Coroutine _sceneLoading;
        
        [SerializeField] private SceneLoading[] loadings;
        [SerializeField] private Dictionary<Scene, string> scenes;

        public SceneArgs SceneArgs { get; private set; }
        
        public void LoadScene<TLoading>(Scene scene, SceneArgs sceneArgs)
            where TLoading : SceneLoading
        {
            if (_sceneLoading == null)
            {
                SceneArgs = sceneArgs;
                
                SceneLoading loading = loadings.OfType<TLoading>().FirstOrDefault();

                if (loading && scenes.TryGetValue(scene, out string sceneName))
                {
                    _sceneLoading = StartCoroutine(LoadScene(sceneName, loading));
                }
            }
            else
            {
                Debug.LogError("Scene already loading.");
            }
        }
        
        private IEnumerator LoadScene(string sceneName, ISceneLoading loading)
        {
            yield return loading.Enable();

            _sceneLoadingHandler = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            yield return _sceneLoadingHandler;

            yield return loading.Disable();
            _sceneLoadingHandler = null;
            _sceneLoading = null;
        }
    }
}