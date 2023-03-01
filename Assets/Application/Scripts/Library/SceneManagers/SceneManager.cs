using System.Collections;
using System.Linq;
using Application.Scripts.Library.SceneManagers.Contracts.Loading;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using UnityEngine;

namespace Application.Scripts.Library.SceneManagers
{
    public class SceneManager : MonoBehaviour
    {
        private AsyncOperation _sceneLoadingHandler;
        
        [SerializeField] private SceneInfo[] scenes;
        [SerializeField] private SceneLoading[] loadings;
        
        public SceneArgs SceneArgs { get; private set; }
        
        public void LoadScene<TScene, TLoading>(SceneArgs sceneArgs) 
            where TScene : SceneInfo
            where TLoading : SceneLoading
        {
            SceneArgs = sceneArgs;

            SceneInfo sceneInfo = scenes.OfType<TScene>().FirstOrDefault();
            SceneLoading loading = loadings.OfType<TLoading>().FirstOrDefault();

            if (sceneInfo && loading)
            {
                StartCoroutine(LoadScene(sceneInfo, loading));
            }
        }
        
        private IEnumerator LoadScene(SceneInfo sceneInfo, ISceneLoading loading)
        {
            yield return loading.Enable();

            _sceneLoadingHandler = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneInfo.SceneName);
            yield return _sceneLoadingHandler;

            yield return loading.Disable();
            _sceneLoadingHandler = null;
        }
    }
}