using System;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.SceneManagers.Loading;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ProjectContext = Application.Scripts.Library.DependencyInjection.ProjectContext;
using Scene = Application.Scripts.Library.SceneManagers.Contracts.SceneInfo.Scene;

namespace Application.Scripts.Application.Scenes.MainMenu.Buttons
{
    public class StartGame : MonoBehaviour
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