using System;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.SceneManagers.Loading;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using UnityEngine;
using UnityEngine.UI;
using Scene = Application.Scripts.Library.SceneManagers.Contracts.SceneInfo.Scene;

namespace Application.Scripts.Application.Scenes.MainMenu.Buttons
{
    public class StartGame : MonoBehaviour, IInitializing
    {
        private ISceneManager _sceneManager;
        
        [SerializeField] private Button button;

        public void Initialize()
        {
            _sceneManager = ProjectContext.Instance.GetService<ISceneManager>();
        }
        
        private void OnEnable()
        {
            button.onClick.AddListener(LoadChoosePack);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(LoadChoosePack);
        }

        private void LoadChoosePack()
        {
            _sceneManager.LoadScene(Scene.ChoosePack);
        }
    }
}