using System;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps.Animators.WinAnimator
{
    [Serializable]
    public struct WinPopUpAnimatorConfig
    {
        [SerializeField] private float popUpDuration;
        [SerializeField] private float titleDuration;
        [SerializeField] private float textDuration;
        [SerializeField] private float energyShowDuration;
        [SerializeField] private float packBackgroundDuration;
        [SerializeField] private float changePackImageDuration;
        [SerializeField] private float buttonDelay;
        [SerializeField] private float hideDelay;

        public float PopUpDuration => popUpDuration;
        public float TitleDuration => titleDuration;
        public float TextDuration => textDuration;
        public float EnergyShowDuration => energyShowDuration;
        public float PackBackgroundDuration => packBackgroundDuration;
        public float ChangePackImageDuration => changePackImageDuration;
        public float ButtonDelay => buttonDelay;
        public float HideDelay => hideDelay;
    }
}