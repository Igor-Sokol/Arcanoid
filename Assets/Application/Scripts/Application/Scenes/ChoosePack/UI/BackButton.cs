using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.SceneManagers.Loading;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ProjectContext = Application.Scripts.Library.DependencyInjection.ProjectContext;

namespace Application.Scripts.Application.Scenes.ChoosePack.UI
{
    public class BackButton : MonoBehaviour
    {
        private ISceneManager _sceneManager;
        
        [SerializeField] private Button button;

        [Inject]
        private void Construct(ISceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }
        
        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _sceneManager.LoadScene(Scene.MainMenu);
        }
    }
}