using System;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using DG.Tweening;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlaceAnimators
{
    public class PackPlaceAnimator : MonoBehaviour
    {
        private Sequence _activeAnimation;

        [SerializeField] private float placeDuration;
        
        public event Action OnEndAnimation;

        public void Place(Block[][] blocks, Vector3[][] positions)
        {
            _activeAnimation?.Kill();
            _activeAnimation = DOTween.Sequence();

            float blockPlaceDuration = placeDuration / blocks.Sum(b => b.Length);
            
            for (int i = 0; i < blocks.Length; i++)
            {
                for (int j = 0; j < blocks[i].Length; j++)
                {
                    blocks[i][j].transform.position = Vector3.zero;
                    _activeAnimation.Append(blocks[i][j].transform.DOMove(positions[i][j], blockPlaceDuration));
                }
            }

            _activeAnimation.AppendCallback(() =>
            {
                OnEndAnimation?.Invoke();
                OnEndAnimation = null;
            });
        }
    }
}