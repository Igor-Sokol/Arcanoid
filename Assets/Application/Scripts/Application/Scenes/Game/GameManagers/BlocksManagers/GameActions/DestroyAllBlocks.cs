using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Sirenix.Utilities;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.GameActions
{
    public class DestroyAllBlocks : IGameAction
    {
        private readonly BlockManager _blockManager;
        private readonly float _lineCooldown;

        private float _timer;
        private int _line;
        
        public DestroyAllBlocks(BlockManager blockManager, float lineCooldown)
        {
            _blockManager = blockManager;
            _lineCooldown = lineCooldown;

            _timer = lineCooldown;
        }
        
        public void OnBegin(ActionInfo actionInfo)
        {
        }

        public void OnUpdate(ActionInfo actionInfo)
        {
            _timer -= actionInfo.DeltaTime;

            if (_timer <= 0 && _line < _blockManager.BlockArray.Length)
            {
                _timer += _lineCooldown;

                foreach (var block in _blockManager.BlockArray[_line])
                {
                    if (block) block.DestroyServiceManager.Destroy();
                }
                _line++;

                if (_line >= _blockManager.BlockArray.Length)
                {
                    actionInfo.ActionHandler.Stop();
                }
            }
        }

        public void OnComplete(ActionInfo actionInfo)
        {
        }

        public void OnStop(ActionInfo actionInfo)
        {
        }
    }
}