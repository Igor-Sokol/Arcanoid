using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.LevelPackManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps;
using Application.Scripts.Application.Scenes.Shared.Energy.Config;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.SceneManagers.Loading;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Contracts;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Localization.LocalizationManagers;
using Application.Scripts.Library.PopUpManagers;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.PopUpManagers
{
    public class WinManager : MonoBehaviour, IInitializing
    {
        private IEnergyManager _energyManager;
        private IPackProgressManager _packProgressManager;
        private ILocalizationManager _localizationManager;
        private IPopUpManager _popUpManager;
        private ISceneManager _sceneManager;
        private WinGamePopUp _winGamePopUp;

        [SerializeField] private LevelPackManager levelPackManager;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private BallsManager ballsManager;
        [SerializeField] private BoostObjectManager boostObjectManager;
        [SerializeField] private BoostManager boostManager;
        [SerializeField] private EnergyValueConfig energyPriceConfig;
        [SerializeField] private BlockProgressManager progressManager;
        
        public void Initialize()
        {
            _sceneManager = ProjectContext.Instance.GetService<ISceneManager>();
            _energyManager = ProjectContext.Instance.GetService<IEnergyManager>();
            _localizationManager = ProjectContext.Instance.GetService<ILocalizationManager>();
            _popUpManager = ProjectContext.Instance.GetService<IPopUpManager>();
            _packProgressManager = ProjectContext.Instance.GetService<IPackProgressManager>();
        }
        
        private void OnEnable()
        {
            progressManager.OnAllBlockBroken += PlayerWin;
        }
        private void OnDisable()
        {
            progressManager.OnAllBlockBroken -= PlayerWin;
        }
        
        private void PlayerWin()
        {
            _packProgressManager.CompleteLevel(levelPackManager.GetCurrentPackInfo());
            ballsManager.PrepareReuse();
            boostObjectManager.PrepareReuse();
            boostManager.PrepareReuse();

            _winGamePopUp = _popUpManager.Get<WinGamePopUp>();
            _winGamePopUp.WinPopUpAnimator.Configure(levelPackManager.GetCurrentPackInfo(), _localizationManager);
            _winGamePopUp.WinPopUpAnimator.Configure(_energyManager.CurrentEnergy, _energyManager.MaxEnergy,
                energyPriceConfig.WinGift);

            _energyManager.AddEnergy(energyPriceConfig.WinGift);
            _winGamePopUp.ContinueActive = levelPackManager.NextLevelExists() &&
                                           _energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice;
            _winGamePopUp.ContinuePrice.SetPrice(energyPriceConfig.LevelPrice);
            
            _winGamePopUp.OnContinueSelected += OnContinue;
            _winGamePopUp.OnMenuSelected += OnMenu;
            
            _winGamePopUp.OnShown += () =>
            {
                _energyManager.OnEnergyAdded += UpdateEnergy;
                _energyManager.OnEnergyRemoved += UpdateEnergy;
                _energyManager.OnFillTimeChanged += UpdateEnergyTime;
            };
            
            _winGamePopUp.OnHidden += () =>
            {
                _energyManager.OnEnergyAdded -= UpdateEnergy;
                _energyManager.OnEnergyRemoved -= UpdateEnergy;
                _energyManager.OnFillTimeChanged -= UpdateEnergyTime;
            };
            
            _winGamePopUp.Show();
        }
        
        private void OnContinue()
        {
            levelPackManager.TrySetNextLevel();
            _winGamePopUp.WinPopUpAnimator.Configure(levelPackManager.GetCurrentPackInfo(), _localizationManager);
            _winGamePopUp.Hide();
            _winGamePopUp.OnHidden += () =>
            {
                levelPackManager.RenderView();
                levelPackManager.RenderView();
                gameplayManager.StartGame(levelPackManager.GetCurrentLevel());
            };
        }
        private void OnMenu()
        {
            _winGamePopUp.Hide();
            _winGamePopUp.OnHidden += () => _sceneManager.LoadScene<DefaultSceneLoading>(Scene.ChoosePack);
        }
        private void UpdateEnergy()
        {
            _winGamePopUp.EnergyView.SetProgress(_energyManager.CurrentEnergy, _energyManager.MaxEnergy);
            _winGamePopUp.ContinueActive = levelPackManager.NextLevelExists() &&
                                           _energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice;
        }
        private void UpdateEnergyTime(float time)
        {
            _winGamePopUp.EnergyView.SetTimeLeft(time);
        }
    }
}