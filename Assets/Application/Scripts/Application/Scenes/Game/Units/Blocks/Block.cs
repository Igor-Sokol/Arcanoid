using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockViews;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks
{
    public class Block : MonoBehaviour, IReusable, IInitializing
    {
        [SerializeField] private string key;
        [SerializeField] private BlockView blockView;
        [SerializeField] private HitServiceManager hitServiceManager;
        [SerializeField] private DestroyServiceManager destroyServiceManager;
        [SerializeField] private BlockServiceManager blockServiceManager;

        public string Key => key;
        public BlockView BlockView => blockView;
        public HitServiceManager HitServiceManager => hitServiceManager;
        public DestroyServiceManager DestroyServiceManager => destroyServiceManager;
        public BlockServiceManager BlockServiceManager => blockServiceManager;

        public void PrepareReuse()
        {
            blockServiceManager.PrepareReuse();
            HitServiceManager.PrepareReuse();
            destroyServiceManager.PrepareReuse();
        }

        public void Initialize()
        {
            destroyServiceManager.Initialize();
        }
    }
}