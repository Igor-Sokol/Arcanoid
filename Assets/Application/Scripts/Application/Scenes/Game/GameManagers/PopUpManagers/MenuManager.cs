using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.LevelPackManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers.GameScales;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps;
using Application.Scripts.Application.Scenes.Shared.Energy.Config;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.SceneManagers.Loading;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.PopUpManagers;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using Application.Scripts.Library.TimeManagers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.PopUpManagers
{
    public class MenuManager : MonoBehaviour, IInitializing
    {
        private IEnergyManager _energyManager;
        private IPopUpManager _popUpManager;
        private ISceneManager _sceneManager;
        private MenuPopUp _menuPopUp;
        private GameTimeScale _gameTimeScale;

        [SerializeField] private Button button;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private LevelPackManager levelPackManager;
        [SerializeField] private TimeScaleManager timeScaleManager;
        [SerializeField] private EnergyPriceConfig energyPriceConfig;

        public void Initialize()
        {
            _energyManager = ProjectContext.Instance.GetService<IEnergyManager>();
            _popUpManager = ProjectContext.Instance.GetService<IPopUpManager>();
            _sceneManager = ProjectContext.Instance.GetService<ISceneManager>();
            _gameTimeScale = timeScaleManager.GetTimeScale<GameTimeScale>();
        }
        
        private void OnEnable()
        {
            button.onClick.AddListener(OpenMenu);
        }

        private void OnDisable()
        {
            button.onClick.AddListener(OpenMenu);
        }

        private void OpenMenu()
        {
            _gameTimeScale.Scale = 0;
            _menuPopUp = _popUpManager.Show<MenuPopUp>();

            _menuPopUp.RestartActive = _energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice;
            _menuPopUp.RestartPrice.SetPrice(energyPriceConfig.LevelPrice);
            
            _menuPopUp.OnRestartSelected += OnRestart;
            _menuPopUp.OnBackSelected += OnMenu;
            _menuPopUp.OnContinueSelected += OnContinue;
            _menuPopUp.OnHidden += () => _gameTimeScale.Scale = 1;
        }
        
        private void OnRestart()
        {
            _menuPopUp.Hide();
            gameplayManager.StartGame(levelPackManager.GetCurrentLevel());
        }

        private void OnMenu()
        {
            _menuPopUp.Hide();
            _sceneManager.LoadScene<DefaultSceneLoading>(Scene.ChoosePack);
        }

        private void OnContinue()
        {
            _menuPopUp.Hide();
        }
    }
}