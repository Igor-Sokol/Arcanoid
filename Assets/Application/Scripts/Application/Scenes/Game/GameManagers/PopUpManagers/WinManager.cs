using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
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
        private IPopUpManager _popUpManager;
        private ISceneManager _sceneManager;
        private WinGamePopUp _winGamePopUp;

        [SerializeField] private LevelPackManager levelPackManager;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private BallsManager ballsManager;
        [SerializeField] private EnergyValueConfig energyPriceConfig;
        [SerializeField] private BlockProgressManager progressManager;
        
        public void Initialize()
        {
            _sceneManager = ProjectContext.Instance.GetService<ISceneManager>();
            _energyManager = ProjectContext.Instance.GetService<IEnergyManager>();
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
            _energyManager.AddEnergy(energyPriceConfig.WinGift);
            ballsManager.PrepareReuse();

            _winGamePopUp = _popUpManager.Show<WinGamePopUp>();
            _winGamePopUp.PrepareReuse();
            _winGamePopUp.Configure(levelPackManager.GetCurrentPackInfo());

            _winGamePopUp.ContinueActive = levelPackManager.TrySetNextLevel() &&
                                           _energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice;
            _winGamePopUp.ContinuePrice.SetPrice(energyPriceConfig.LevelPrice);
            
            _winGamePopUp.OnContinueSelected += OnContinue;
            _winGamePopUp.OnMenuSelected += OnMenu;
        }
        
        private void OnContinue()
        {
            _winGamePopUp.Hide();
            levelPackManager.RenderView();
            gameplayManager.StartGame(levelPackManager.GetCurrentLevel());
        }

        private void OnMenu()
        {
            _winGamePopUp.Hide();
            _sceneManager.LoadScene<DefaultSceneLoading>(Scene.ChoosePack);
        }
    }
}