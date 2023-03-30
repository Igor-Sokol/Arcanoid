using Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.LevelPackManagers;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps.LosePopUp;
using Application.Scripts.Application.Scenes.Shared.Energy.Config;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.UI.PopUps.MessagePopUp;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Localization.LocalizationManagers;
using Application.Scripts.Library.PopUpManagers;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using Plugins.MobileBlur;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.PopUpManagers
{
    public class LoseManager : MonoBehaviour, IInitializing
    {
        private IBlur _blur;
        private ISceneManager _sceneManager;
        private IEnergyManager _energyManager;
        private IPopUpManager _popUpManager;
        private LoseGamePopUp _loseGamePopUp;
        private ILocalizationManager _localizationManager;
        
        [SerializeField] private LevelPackManager levelPackManager;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private HealthManager healthManager;
        [SerializeField] private BallsManager ballsManager;
        [SerializeField] private BoostObjectManager boostObjectManager;
        [SerializeField] private BoostManager boostManager;
        [SerializeField] private EnergyValueConfig energyPriceConfig;
        [SerializeField] private ActiveBallManager activeBallManager;
        [SerializeField] private string noEnergyKey;

        public void Initialize()
        {
            _sceneManager = ProjectContext.Instance.GetService<ISceneManager>();
            _energyManager = ProjectContext.Instance.GetService<IEnergyManager>();
            _popUpManager = ProjectContext.Instance.GetService<IPopUpManager>();
            _blur = ProjectContext.Instance.GetService<IBlur>();
            _localizationManager = ProjectContext.Instance.GetService<ILocalizationManager>();
        }

        public void PlayerLose()
        {
            ballsManager.PrepareReuse();
            boostObjectManager.PrepareReuse();
            boostManager.PrepareReuse();
            activeBallManager.PrepareReuse();
            
            _loseGamePopUp = _popUpManager.Get<LoseGamePopUp>();
            _loseGamePopUp.LosePopUpAnimator.Configure(_energyManager.CurrentEnergy, _energyManager.MaxEnergy);

            _loseGamePopUp.HealthPrice.SetPrice(energyPriceConfig.HealthPrice);
            _loseGamePopUp.RestartPrice.SetPrice(energyPriceConfig.LevelPrice);
            
            _loseGamePopUp.OnAddHealthSelected += OnAddHealth;
            _loseGamePopUp.OnRestartSelected += OnRestart;
            _loseGamePopUp.OnMenuSelected += OnMenu;

            _loseGamePopUp.OnShown += () =>
            {
                _energyManager.OnEnergyAdded += UpdateEnergy;
                _energyManager.OnEnergyRemoved += UpdateEnergy;
                _energyManager.OnFillTimeChanged += UpdateEnergyTime;
            };
            
            _loseGamePopUp.OnHidden += () =>
            {
                _energyManager.OnEnergyAdded -= UpdateEnergy;
                _energyManager.OnEnergyRemoved -= UpdateEnergy;
                _energyManager.OnFillTimeChanged -= UpdateEnergyTime;
            };
            
            _loseGamePopUp.Show();
            _blur.Enable();
        }
        
        private void OnAddHealth()
        {
            if (_energyManager.CurrentEnergy >= energyPriceConfig.HealthPrice)
            {
                _loseGamePopUp.Hide();
                _blur.Disable();
                _energyManager.RemoveEnergy(energyPriceConfig.HealthPrice);
                healthManager.AddHealth();
                _loseGamePopUp.OnHidden += () => gameplayManager.SetBall();
            }
            else
            {
                ShowNoEnergyPopUp();
            }
        }
        private void OnRestart()
        {
            if (_energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice)
            {
                _loseGamePopUp.Hide();
                _blur.Disable();
                _loseGamePopUp.OnHidden += () => gameplayManager.StartGame(levelPackManager.GetCurrentLevel());
            }
            else
            {
                ShowNoEnergyPopUp();
            }
        }
        private void OnMenu()
        {
            _loseGamePopUp.Hide();
            _loseGamePopUp.OnHidden += () => _sceneManager.LoadScene(Scene.ChoosePack);
        }
        private void UpdateEnergy()
        {
            _loseGamePopUp.EnergyView.SetProgress(_energyManager.CurrentEnergy, _energyManager.MaxEnergy);
            _loseGamePopUp.AddHealthActive = _energyManager.CurrentEnergy >= energyPriceConfig.HealthPrice;
            _loseGamePopUp.RestartActive = _energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice;
        }
        private void UpdateEnergyTime(float time)
        {
            _loseGamePopUp.EnergyView.SetTimeLeft(time);
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