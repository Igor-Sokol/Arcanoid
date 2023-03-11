using Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Objects;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PlayingField.Borders
{
    public class BoostObjectDeadZone : MonoBehaviour
    {
        [SerializeField] private BoostObjectManager boostObjectManager;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<BoostObject>(out var boostObject))
            {
                boostObjectManager.ReturnBoostView(boostObject);
            }
        }
    }
}