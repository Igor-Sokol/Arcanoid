using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.LevelPackManagers;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps;
using Application.Scripts.Application.Scenes.Game.Units.Platform;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.SceneManagers.Loading;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Contracts;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.PopUpManagers;
using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers
{
    public class GameProcessManager : MonoBehaviour, IInitializing
    {
        private IPackProgressManager _packProgressManager;
        private IPopUpManager _popUpManager;
        private ISceneManager _sceneManager;
        private IPopUp _activePopUp;
        private LoseGamePopUp _loseGamePopUp;
        private WinGamePopUp _winGamePopUp;

        [SerializeField] private LevelPackManager levelPackManager;
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private BlockProgressManager blockProgressManager;
        [SerializeField] private HealthManager healthManager;
        [SerializeField] private BallsManager ballsManager;
        [SerializeField] private Platform platform;
        
        public void Initialize()
        {
            _packProgressManager = ProjectContext.Instance.GetService<IPackProgressManager>();
            _popUpManager = ProjectContext.Instance.GetService<IPopUpManager>();
            _sceneManager = ProjectContext.Instance.GetService<ISceneManager>();
        }
        private void OnEnable()
        {
            ballsManager.OnAllBallRemoved += OnMissedBalls;
            healthManager.OnDead += OnPlayerLose;
            blockProgressManager.OnAllBlockBroken += OnPlayerWin;
        }
        private void OnDisable()
        {
            ballsManager.OnAllBallRemoved -= OnMissedBalls;
            healthManager.OnDead -= OnPlayerLose;
            blockProgressManager.OnAllBlockBroken -= OnPlayerWin;
        }
        
        private void OnMissedBalls()
        {
            healthManager.RemoveHealth();

            if (healthManager.CurrentHealth > 0)
            {
                platform.BallLauncher.SetBall(ballsManager.GetBall());
            }
        }

        private void OnPlayerLose()
        {
            _loseGamePopUp = _popUpManager.Show<LoseGamePopUp>();
            _loseGamePopUp.OnRestartSelected += OnRestart;
            _loseGamePopUp.OnMenuSelected += OnMenu;

            _activePopUp = _loseGamePopUp;
        }

        private void OnPlayerWin()
        {
            _packProgressManager.CompleteLevel(levelPackManager.GetCurrentPackInfo());
            ballsManager.PrepareReuse();

            _winGamePopUp = _popUpManager.Show<WinGamePopUp>();
            _winGamePopUp.Configure(levelPackManager.GetCurrentPackInfo());

            if (!levelPackManager.TrySetNextLevel())
            {
                _winGamePopUp.ContinueButton.interactable = false;
            }
            
            _winGamePopUp.OnContinueSelected += OnContinue;
            _winGamePopUp.OnMenuSelected += OnMenu;

            _activePopUp = _winGamePopUp;
        }

        private void OnRestart()
        {
            _activePopUp.Hide();
            gameplayManager.StartGame(levelPackManager.GetCurrentLevel());
        }
        
        private void OnContinue()
        {
            _activePopUp.Hide();
            gameplayManager.StartGame(levelPackManager.GetCurrentLevel());
        }

        private void OnMenu()
        {
            _activePopUp.Hide();
            _sceneManager.LoadScene<DefaultSceneLoading>(Scene.ChoosePack);
        }
    }
}