using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.LevelPackManagers;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps;
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
            _loseGamePopUp = _popUpManager.Show<LoseGamePopUp>();

            _loseGamePopUp.AddHealthActive = _energyManager.CurrentEnergy >= energyPriceConfig.HealthPrice;
            _loseGamePopUp.RestartActive = _energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice;

            _loseGamePopUp.HealthPrice.SetPrice(energyPriceConfig.HealthPrice);
            _loseGamePopUp.RestartPrice.SetPrice(energyPriceConfig.LevelPrice);
            
            _loseGamePopUp.OnAddHealthSelected += OnAddHealth;
            _loseGamePopUp.OnRestartSelected += OnRestart;
            _loseGamePopUp.OnMenuSelected += OnMenu;
        }
        
        private void OnAddHealth()
        {
            _loseGamePopUp.Hide();
            _energyManager.RemoveEnergy(energyPriceConfig.HealthPrice);
            healthManager.AddHealth();
            gameplayManager.SetBall();
        }
        
        private void OnRestart()
        {
            _loseGamePopUp.Hide();
            gameplayManager.StartGame(levelPackManager.GetCurrentLevel());
        }
        
        private void OnMenu()
        {
            _loseGamePopUp.Hide();
            _sceneManager.LoadScene<DefaultSceneLoading>(Scene.ChoosePack);
        }
    }
}