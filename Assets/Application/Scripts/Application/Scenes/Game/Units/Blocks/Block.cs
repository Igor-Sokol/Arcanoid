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

        public string Key => key;
        public BlockView BlockView => blockView;
        public HitServiceManager HitServiceManager => hitServiceManager;

        public void PrepareReuse()
        {
            HitServiceManager.PrepareReuse();
            destroyServiceManager.PrepareReuse();
        }

        public void Initialize()
        {
            destroyServiceManager.Initialize();
        }
    }
}