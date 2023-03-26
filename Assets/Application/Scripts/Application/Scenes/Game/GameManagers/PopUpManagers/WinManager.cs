using Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.LevelPackManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.PopUpManagers.GameActions;
using Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Application.Scenes.Game.Screen.PlayingField.Effectors;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps;
using Application.Scripts.Application.Scenes.Shared.Energy.Config;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Contracts;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Localization.LocalizationManagers;
using Application.Scripts.Library.PopUpManagers;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using Plugins.MobileBlur;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ProjectContext = Application.Scripts.Library.DependencyInjection.ProjectContext;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.PopUpManagers
{
    public class WinManager : MonoBehaviour, IInitializing, IReusable
    {
        private IBlur _blur;
        private IGameActionManager _gameActionManager;
        private IEnergyManager _energyManager;
        private IPackProgressManager _packProgressManager;
        private ILocalizationManager _localizationManager;
        private IPopUpManager _popUpManager;
        private ISceneManager _sceneManager;
        private WinGamePopUp _winGamePopUp;
        private ActionHandler _freezeAction;

        [SerializeField] private LevelPackManager levelPackManager;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private GameProcessManager gameProcessManager;
        [SerializeField] private BallsManager ballsManager;
        [SerializeField] private BoostObjectManager boostObjectManager;
        [SerializeField] private BoostManager boostManager;
        [SerializeField] private EnergyValueConfig energyPriceConfig;
        [SerializeField] private BlockProgressManager progressManager;
        [SerializeField] private ActiveBallManager activeBallManager;
        [SerializeField] private GameTimeScale gameTimeScale;
        [SerializeField] private ActionTimeManager actionTimeManager;
        [SerializeField] private EffectorManager effectorManager;
        [SerializeField] private Image menuRayBlock;
        [SerializeField] private float freezeTime;
        [SerializeField] private Vector2 freezeScale;

        [Inject]
        private void Construct(IPopUpManager popUpManager, ILocalizationManager localizationManager,
            ISceneManager sceneManager, IGameActionManager gameActionManager, IEnergyManager energyManager,
            IPackProgressManager packProgressManager)
        {
            _popUpManager = popUpManager;
            _localizationManager = localizationManager;
            _sceneManager = sceneManager;
            _gameActionManager = gameActionManager;
            _energyManager = energyManager;
            _packProgressManager = packProgressManager;
        }
        
        public void Initialize()
        {
            _blur = ProjectContext.Instance.GetService<IBlur>();
        }
        
        private void OnEnable()
        {
            progressManager.OnAllBlockBroken += PlayerWin;
        }
        private void OnDisable()
        {
            progressManager.OnAllBlockBroken -= PlayerWin;
            _freezeAction.Stop();
        }
        
        private void PlayerWin()
        {
            effectorManager.Play();
            var winGameAction = new WinGameAction(gameTimeScale, freezeScale);
            _freezeAction = _gameActionManager.StartAction(winGameAction, freezeTime, actionTimeManager);
            gameProcessManager.IgnoreBall = true;
            menuRayBlock.enabled = true;
            
            winGameAction.OnCompleteAction += () =>
            {
                gameTimeScale.Scale = 1f;
                gameProcessManager.IgnoreBall = false;
                menuRayBlock.enabled = false;
                ShowPopUp();
                effectorManager.Stop();
            };
        }

        private void ShowPopUp()
        {
            _packProgressManager.CompleteLevel(levelPackManager.GetCurrentPackInfo());
            ballsManager.PrepareReuse();
            boostObjectManager.PrepareReuse();
            boostManager.PrepareReuse();
            activeBallManager.PrepareReuse();

            _winGamePopUp = _popUpManager.Get<WinGamePopUp>();
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

            var currentPack = levelPackManager.GetCurrentPackInfo();
            var nextPack = currentPack;
            if (levelPackManager.TrySetNextLevel())
            {
                nextPack = levelPackManager.GetCurrentPackInfo();
            }
            _winGamePopUp.WinPopUpAnimator.Configure(currentPack, nextPack, _localizationManager);
            
            _winGamePopUp.Show();
            _blur.Enable();
        }
        private void OnContinue()
        {
            _winGamePopUp.Hide();
            _blur.Disable();
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
            _winGamePopUp.OnHidden += () => _sceneManager.LoadScene(Scene.ChoosePack);
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

        public void PrepareReuse()
        {
            _freezeAction.Stop();
            _blur.Disable();
        }
    }
}