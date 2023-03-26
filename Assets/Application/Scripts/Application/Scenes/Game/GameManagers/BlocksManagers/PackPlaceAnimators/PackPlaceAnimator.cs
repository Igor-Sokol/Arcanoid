using System;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Application.Scenes.Shared.DoTweenGameActions;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.InitializeManager.Contracts;
using DG.Tweening;
using UnityEngine;
using Zenject;
using ProjectContext = Application.Scripts.Library.DependencyInjection.ProjectContext;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlaceAnimators
{
    public class PackPlaceAnimator : MonoBehaviour
    {
        private IGameActionManager _gameActionManager;
        private Sequence _activeAnimation;
        private ActionHandler _animation;

        [SerializeField] private float placeDuration;
        [SerializeField] private ActionTimeManager timeManager;
        
        public event Action OnEndAnimation;
        
        [Inject]
        private void Construct(IGameActionManager gameActionManager)
        {
            _gameActionManager = gameActionManager;
        }

        public void Place(Block[][] blocks, Vector3[][] positions)
        {
            StopTweenAction();
            
            _activeAnimation?.Kill();
            _activeAnimation = DOTween.Sequence();

            float blockPlaceDuration = placeDuration / blocks.Sum(b => b.Length);
            
            for (int i = 0; i < blocks.Length; i++)
            {
                for (int j = 0; j < blocks[i].Length; j++)
                {
                    if (blocks[i][j])
                    {
                        blocks[i][j].transform.position = Vector3.zero;
                        _activeAnimation.Append(blocks[i][j].transform.DOMove(positions[i][j], blockPlaceDuration));
                    }
                }
            }

            _activeAnimation.AppendCallback(() =>
            {
                OnEndAnimation?.Invoke();
                OnEndAnimation = null;
            });
            _activeAnimation.OnComplete(StopTweenAction);

            _animation = _gameActionManager.StartAction(new DoTweenGameAction(_activeAnimation), -1, timeManager);
        }

        private void OnDisable()
        {
            StopTweenAction();
        }

        private void StopTweenAction() => _animation.Stop();
    }
}