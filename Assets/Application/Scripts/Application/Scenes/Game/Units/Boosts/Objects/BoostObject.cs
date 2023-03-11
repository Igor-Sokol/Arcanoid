using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Reusable;
using UnityEngine;
using UnityEngine.Serialization;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Objects
{
    public class BoostObject : MonoBehaviour, IInitializing, IReusable
    {
        [SerializeField] private string key;
        [SerializeField] private BoostObjectAnimator boostObjectAnimator;
        [SerializeField] private Boost[] boosts;

        public string Key => key;
        public Boost[] Boosts => boosts;

        public void Initialize()
        {
            boostObjectAnimator.Initialize();
        }

        public void PrepareReuse()
        {
            boostObjectAnimator.PrepareReuse();
        }
    }
}