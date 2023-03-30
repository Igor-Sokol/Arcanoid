using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.LevelPackManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps.MenuPopUp;
using Application.Scripts.Application.Scenes.Shared.Energy.Config;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.UI.PopUps.MessagePopUp;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Localization.LocalizationManagers;
using Application.Scripts.Library.PopUpManagers;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using Application.Scripts.Library.TimeManagers;
using Plugins.MobileBlur;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.PopUpManagers
{
    public class MenuManager : MonoBehaviour, IInitializing
    {
        private IBlur _blur;
        private IEnergyManager _energyManager;
        private IPopUpManager _popUpManager;
        private ISceneManager _sceneManager;
        private MenuPopUp _menuPopUp;
        private GameTimeScale _gameTimeScale;
        private PauseTimeScale _pauseTimeScale;
        private ILocalizationManager _localizationManager;

        [SerializeField] private Button button;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private LevelPackManager levelPackManager;
        [SerializeField] private TimeScaleManager timeScaleManager;
        [SerializeField] private EnergyValueConfig energyPriceConfig;
        [SerializeField] private BlockManager blockManager;
        [SerializeField] private string noEnergyKey;

        public void Initialize()
        {
            _blur = ProjectContext.Instance.GetService<IBlur>();
            _energyManager = ProjectContext.Instance.GetService<IEnergyManager>();
            _popUpManager = ProjectContext.Instance.GetService<IPopUpManager>();
            _sceneManager = ProjectContext.Instance.GetService<ISceneManager>();
            _gameTimeScale = timeScaleManager.GetTimeScale<GameTimeScale>();
            _pauseTimeScale = timeScaleManager.GetTimeScale<PauseTimeScale>();
            _localizationManager = ProjectContext.Instance.GetService<ILocalizationManager>();
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

            //_menuPopUp.RestartActive = _energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice;
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
            if (_energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice)
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
            else
            {
                ShowNoEnergyPopUp();
            }
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
            if (_energyManager.CurrentEnergy >= energyPriceConfig.SkipPrice)
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
            else
            {
                ShowNoEnergyPopUp();
            }
        }
        
        private void ShowNoEnergyPopUp()
        {
            var messagePopup = _popUpManager.Get<MessagePopUp>();
            messagePopup.Configure(_localizationManager.GetString(noEnergyKey));

            messagePopup.OnContinueSelected += messagePopup.Hide;
            
            messagePopup.Show();
        }
    }
}