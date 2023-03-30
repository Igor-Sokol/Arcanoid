using System;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Application.Scenes.Shared.DoTweenGameActions;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.InitializeManager.Contracts;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlaceAnimators
{
    public class PackPlaceAnimator : MonoBehaviour, IInitializing
    {
        private IGameActionManager _gameActionManager;
        private Sequence _activeAnimation;
        private ActionHandler _animation;

        [SerializeField] private float blockPlaceDuration;
        [SerializeField] private float placeDuration;
        [SerializeField] private Vector3 additionScale;
        [SerializeField] private ActionTimeManager timeManager;
        
        public event Action OnEndAnimation;
        
        public void Initialize()
        {
            _gameActionManager = ProjectContext.Instance.GetService<IGameActionManager>();
            DOTween.SetTweensCapacity(500, 50);
        }

        public void Place(Block[][] blocks, Vector3[][] positions)
        {
            StopTweenAction();
            
            _activeAnimation?.Kill();
            _activeAnimation = DOTween.Sequence();
            
            var sum = blocks.Sum(b => b.Length);
            var blockTime = placeDuration / sum;
            float blockDuration = blockPlaceDuration * sum > blockTime ? blockTime : blockPlaceDuration;

            for (int i = 0; i < blocks.Length; i++)
            {
                Sequence rowAnimation = DOTween.Sequence();
                rowAnimation.AppendInterval(blockDuration * i);
                for (int j = 0; j < blocks[i].Length; j++)
                {
                    var block = blocks[i][j];
                    
                    if (block)
                    {
                        var blockTransform = block.transform;
                        var blockScale = blockTransform.localScale;
                        var targetScale = blockScale;
                        
                        blockScale += additionScale;
                        
                        blockTransform.localScale = blockScale;
                        block.BlockView.Sprites.ForEach(image => image.color = Color.clear);

                        rowAnimation.AppendInterval(blockDuration);
                        block.BlockView.Sprites.ForEach(image =>
                        {
                            rowAnimation.Join(image.DOColor(Color.white, blockDuration)
                                    .SetEase(Ease.InOutQuad));
                            rowAnimation.Join(blockTransform.DOScale(targetScale, blockDuration)
                                .SetEase(Ease.InOutQuad));
                        });
                    }
                }

                _activeAnimation.Join(rowAnimation).SetEase(Ease.Linear);
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