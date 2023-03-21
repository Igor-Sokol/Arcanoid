using System;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.LosePopUp.Animators
{
    [Serializable]
    public struct LosePopUpAnimatorConfig
    {
        [SerializeField] private float popUpDuration;
        [SerializeField] private float textDuration;
        [SerializeField] private float energyShowDuration;
        [SerializeField] private float buttonDelay;
        [SerializeField] private float hideDelay;

        public float PopUpDuration => popUpDuration;
        public float TextDuration => textDuration;
        public float EnergyShowDuration => energyShowDuration;
        public float ButtonDelay => buttonDelay;
        public float HideDelay => hideDelay;
    }
}