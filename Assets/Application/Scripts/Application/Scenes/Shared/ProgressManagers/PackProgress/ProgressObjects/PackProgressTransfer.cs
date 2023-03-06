namespace Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.ProgressObjects
{
    public struct PackProgressTransfer
    {
        public int PackIndex { get; private set; }
        public int CurrentLevelIndex { get; private set; }

        public PackProgressTransfer(int packIndex, int currentLevelIndex)
        {
            PackIndex = packIndex;
            CurrentLevelIndex = currentLevelIndex;
        }
    }
}