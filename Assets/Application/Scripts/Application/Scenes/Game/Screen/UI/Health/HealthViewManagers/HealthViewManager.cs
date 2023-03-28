using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.Screen.UI.Health.HealthViews;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.ObjectPools;
using Application.Scripts.Library.Reusable;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Screen.UI.Health.HealthViewManagers
{
    public class HealthViewManager : MonoBehaviour, IInitializing, IReusable
    {
        private readonly List<HealthView> _healthViews = new List<HealthView>();
        private ObjectPoolMono<HealthView> _healthViewPool;
        private Vector2 _viewImageSize;
        private IHealthManager _healthManager;

        [SerializeField] private HealthView healthViewPrefab;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private Transform container;
        [SerializeField] private Vector2 spacePercentage;

        [Inject]
        private void Construct(IHealthManager healthManager)
        {
            _healthManager = healthManager;
        }
        
        public void Initialize()
        {
            _healthViewPool = new ObjectPoolMono<HealthView>(() => Instantiate(healthViewPrefab, container), container);
            _healthManager.OnPrepareReuse += PrepareReuse;
            PrepareReuse();
        }
        
        public void PrepareReuse()
        {
            foreach (var healthView in _healthViews)
            {
                _healthViewPool.Return(healthView);
            }
            _healthViews.Clear();
            
            SetViewScale(_healthManager.MaxHealth);
            GenerateViews(_healthManager.MaxHealth);
            InitViews(_healthManager.CurrentHealth);
        }

        private void OnEnable()
        {
            _healthManager.OnHealthAdded += OnHealthAdded;
            _healthManager.OnHealthRemoved += OnHealthRemoved;
        }

        private void OnDisable()
        {
            _healthManager.OnHealthAdded -= OnHealthAdded;
            _healthManager.OnHealthRemoved -= OnHealthRemoved;
        }

        private void InitViews(int currentHealth)
        {
            for (int i = 0; i < currentHealth; i++)
            {
                OnHealthAdded();
            }
        }
        
        private void GenerateViews(int viewCount)
        {
            for (int i = 0; i < viewCount; i++)
            {
                var view = _healthViewPool.Get();
                view.PrepareReuse();
                view.transform.SetSiblingIndex(i);
                _healthViews.Add(view);
            }
        }

        private void SetViewScale(int viewCount)
        {
            _viewImageSize = healthViewPrefab.Sprite.rect.size;
            gridLayoutGroup.spacing = spacePercentage * rectTransform.rect.size;
            
            int weigh = Mathf.Clamp(viewCount, 0, gridLayoutGroup.constraintCount);
            int height = Mathf.CeilToInt(viewCount / (float)gridLayoutGroup.constraintCount);
            
            Vector2 contentSpace;
            contentSpace.x = weigh * gridLayoutGroup.spacing.x;
            contentSpace.y = height * gridLayoutGroup.spacing.y;
            
            Vector2 spaceSize = rectTransform.rect.size - contentSpace;
            spaceSize /= new Vector2(weigh, height);

            Vector2 scales = new Vector2(spaceSize.x / _viewImageSize.x, spaceSize.y / _viewImageSize.y);

            float scale = scales.x < scales.y ? scales.x : scales.y;

            gridLayoutGroup.cellSize = new Vector2(_viewImageSize.x * scale, _viewImageSize.y * scale);
        }

        private void OnHealthAdded()
        {
            _healthViews.LastOrDefault(h => !h.Enabled)?.Enable();
        }
        
        private void OnHealthRemoved()
        {
            _healthViews.FirstOrDefault(h => h.Enabled)?.Disable();
        }
    }
}