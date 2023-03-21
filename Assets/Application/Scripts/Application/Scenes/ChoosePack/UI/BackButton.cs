using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.SceneManagers.Loading;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.ChoosePack.UI
{
    public class BackButton : MonoBehaviour
    {
        [SerializeField] private Button button;

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
            ProjectContext.Instance.GetService<ISceneManager>().LoadScene(Scene.MainMenu);
        }
    }
}