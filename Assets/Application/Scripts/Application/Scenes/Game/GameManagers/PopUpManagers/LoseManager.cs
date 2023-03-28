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
using Application.Scripts.Library.PopUpManagers;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using Plugins.MobileBlur;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.PopUpManagers
{
    public class LoseManager : MonoBehaviour
    {
        private IBlur _blur;
        private ISceneManager _sceneManager;
        private IEnergyManager _energyManager;
        private IPopUpManager _popUpManager;
        private LoseGamePopUp _loseGamePopUp;
        
        [SerializeField] private LevelPackManager levelPackManager;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private HealthManager healthManager;
        [FormerlySerializedAs("ballsManager")] [SerializeField] private BallManager ballManager;
        [SerializeField] private BoostObjectManager boostObjectManager;
        [SerializeField] private BoostManager boostManager;
        [SerializeField] private EnergyValueConfig energyPriceConfig;
        [SerializeField] private ActiveBallManager activeBallManager;

        [Inject]
        private void Construct(IPopUpManager popUpManager, ISceneManager sceneManager, IEnergyManager energyManager,
            IBlur blur)
        {
            _popUpManager = popUpManager;
            _sceneManager = sceneManager;
            _energyManager = energyManager;
            _blur = blur;
        }

        public void PlayerLose()
        {
            ballManager.PrepareReuse();
            boostObjectManager.PrepareReuse();
            boostManager.PrepareReuse();
            activeBallManager.PrepareReuse();
            
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
            _blur.Enable();
        }
        
        private void OnAddHealth()
        {
            _loseGamePopUp.Hide();
            _blur.Disable();
            _energyManager.RemoveEnergy(energyPriceConfig.HealthPrice);
            healthManager.AddHealth();
            _loseGamePopUp.OnHidden += () => gameplayManager.SetBall();
        }
        private void OnRestart()
        {
            _loseGamePopUp.Hide();
            _blur.Disable();
            _loseGamePopUp.OnHidden += () => gameplayManager.StartGame(levelPackManager.GetCurrentLevel());
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
    }
}