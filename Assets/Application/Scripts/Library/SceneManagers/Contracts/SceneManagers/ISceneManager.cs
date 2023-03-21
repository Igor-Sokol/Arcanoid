using Application.Scripts.Library.SceneManagers.Contracts.Loading;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;

namespace Application.Scripts.Library.SceneManagers.Contracts.SceneManagers
{
    public interface ISceneManager
    {
        void LoadScene(Scene scene);
        void LoadScene<TLoading>(Scene scene) where TLoading : SceneLoading;
    }
}