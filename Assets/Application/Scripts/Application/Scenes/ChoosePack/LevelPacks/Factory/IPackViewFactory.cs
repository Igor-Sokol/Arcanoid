using UnityEngine;

namespace Application.Scripts.Application.Scenes.ChoosePack.LevelPacks.Factory
{
    public interface IPackViewFactory
    {
        PackView Create(PackView prefab, Transform container);
    }
}