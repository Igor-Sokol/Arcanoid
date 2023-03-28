using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.LevelPackManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps.MenuPopUp;
using Application.Scripts.Application.Scenes.Shared.Energy.Config;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.PopUpManagers;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using Application.Scripts.Library.TimeManagers;
using Application.Scripts.Library.TimeManagers.Contracts;
using Plugins.MobileBlur;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.PopUpManagers
{
    public class MenuManager : MonoBehaviour, IInitializing
    {
        private IBlur _blur;
        private IEnergyManager _energyManager;
        private IPopUpManager _popUpManager;
        private ISceneManager _sceneManager;
        private ITimeScaleManager _timeScaleManager;
        private MenuPopUp _menuPopUp;
        private GameTimeScale _gameTimeScale;
        private PauseTimeScale _pauseTimeScale;

        [SerializeField] private Button button;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private LevelPackManager levelPackManager;
        [SerializeField] private EnergyValueConfig energyPriceConfig;
        [SerializeField] private BlockManager blockManager;

        [Inject]
        private void Construct(IPopUpManager popUpManager, ISceneManager sceneManager, IEnergyManager energyManager,
            IBlur blur, ITimeScaleManager timeScaleManager)
        {
            _popUpManager = popUpManager;
            _sceneManager = sceneManager;
            _energyManager = energyManager;
            _blur = blur;
            _timeScaleManager = timeScaleManager;
        }
        
        public void Initialize()
        {
            _gameTimeScale = _timeScaleManager.GetTimeScale<GameTimeScale>();
            _pauseTimeScale = _timeScaleManager.GetTimeScale<PauseTimeScale>();
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
            _pauseTimeScale.Scale = 0;
            _menuPopUp = _popUpManager.Get<MenuPopUp>();

            _menuPopUp.RestartActive = _energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice;
            _menuPopUp.RestartPrice.SetPrice(energyPriceConfig.LevelPrice);
            _menuPopUp.SkipActive = _energyManager.CurrentEnergy >= energyPriceConfig.SkipPrice;
            _menuPopUp.SkipPrice.SetPrice(energyPriceConfig.SkipPrice);
            
            _menuPopUp.OnRestartSelected += OnRestart;
            _menuPopUp.OnBackSelected += OnMenu;
            _menuPopUp.OnContinueSelected += OnContinue;
            _menuPopUp.OnSkipSelected += OnSkip;

            _menuPopUp.Show();
            _blur.Enable();
        }
        
        private void OnRestart()
        {
            _menuPopUp.OnHidden += () =>
            {
                _gameTimeScale.Scale = 1;
                _pauseTimeScale.Scale = 1;
            };
            _menuPopUp.Hide();
            _blur.Disable();
            gameplayManager.StartGame(levelPackManager.GetCurrentLevel());
        }

        private void OnMenu()
        {
            _menuPopUp.Hide();
            _menuPopUp.OnHidden += () => _sceneManager.LoadScene(Scene.ChoosePack);
        }

        private void OnContinue()
        {
            _menuPopUp.OnHidden += () =>
            {
                _gameTimeScale.Scale = 1;
                _pauseTimeScale.Scale = 1;
            };
            _menuPopUp.Hide();
            _blur.Disable();
        }

        private void OnSkip()
        {
            _menuPopUp.OnHidden += () =>
            {
                _gameTimeScale.Scale = 1;
                _pauseTimeScale.Scale = 1;
            };
            _menuPopUp.Hide();
            _blur.Disable();
            _energyManager.RemoveEnergy(energyPriceConfig.SkipPrice);
            blockManager.DestroyAllBlocks();
        }
    }
}