using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Objects;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Platform.PlatformComponents
{
    public class BoostObjectHandler : MonoBehaviour
    {
        [SerializeField] private BoostManager boostManager;
        [SerializeField] private BoostObjectManager boostObjectManager;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<BoostObject>(out var boostObject))
            {
                boostManager.Execute(boostObject.Boosts);
                boostObjectManager.ReturnBoostView(boostObject);
            }
        }
    }
}