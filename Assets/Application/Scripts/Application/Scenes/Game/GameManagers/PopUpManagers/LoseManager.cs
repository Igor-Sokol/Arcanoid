using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.LevelPackManagers;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps.LosePopUp;
using Application.Scripts.Application.Scenes.Shared.Energy.Config;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.SceneManagers.Loading;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.PopUpManagers;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.PopUpManagers
{
    public class LoseManager : MonoBehaviour, IInitializing
    {
        private ISceneManager _sceneManager;
        private IEnergyManager _energyManager;
        private IPopUpManager _popUpManager;
        private LoseGamePopUp _loseGamePopUp;
        
        [SerializeField] private LevelPackManager levelPackManager;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private HealthManager healthManager;
        [SerializeField] private EnergyValueConfig energyPriceConfig;

        public void Initialize()
        {
            _sceneManager = ProjectContext.Instance.GetService<ISceneManager>();
            _energyManager = ProjectContext.Instance.GetService<IEnergyManager>();
            _popUpManager = ProjectContext.Instance.GetService<IPopUpManager>();
        }
        
        private void OnEnable()
        {
            healthManager.OnDead += PlayerLose;
        }
        private void OnDisable()
        {
            healthManager.OnDead -= PlayerLose;
        }
        
        private void PlayerLose()
        {
            _loseGamePopUp = _popUpManager.Get<LoseGamePopUp>();
            _loseGamePopUp.LosePopUpAnimator.Configure(_energyManager.CurrentEnergy, _energyManager.MaxEnergy);

            _loseGamePopUp.AddHealthActive = _energyManager.CurrentEnergy >= energyPriceConfig.HealthPrice;
            _loseGamePopUp.RestartActive = _energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice;

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
        }
        
        private void OnAddHealth()
        {
            _loseGamePopUp.Hide();
            _energyManager.RemoveEnergy(energyPriceConfig.HealthPrice);
            healthManager.AddHealth();
            _loseGamePopUp.OnHidden += () => gameplayManager.SetBall();
        }
        private void OnRestart()
        {
            _loseGamePopUp.Hide();
            _loseGamePopUp.OnHidden += () => gameplayManager.StartGame(levelPackManager.GetCurrentLevel());
        }
        private void OnMenu()
        {
            _loseGamePopUp.Hide();
            _loseGamePopUp.OnHidden += () => _sceneManager.LoadScene<DefaultSceneLoading>(Scene.ChoosePack);
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
    }
}