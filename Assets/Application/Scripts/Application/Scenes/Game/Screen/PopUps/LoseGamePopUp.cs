using System;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps
{
    public class LoseGamePopUp : PopUp
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;

        public override bool Active => gameObject.activeSelf;
        public override IPopUpAnimator PopUpAnimator { get; set; }
        public override event Action OnShown;
        public override event Action OnHidden;
        
        public event Action OnRestartSelected;
        public event Action OnMenuSelected;
        public override void Show()
        {
            gameObject.SetActive(true);
            OnShown?.Invoke();
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            OnHidden?.Invoke();

            OnHidden = null;
            OnShown = null;
            OnRestartSelected = null;
            OnMenuSelected = null;
        }

        private void OnEnable()
        {
            restartButton.onClick.AddListener(OnRestart);
            menuButton.onClick.AddListener(OnMenu);
        }
        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(OnRestart);
            menuButton.onClick.RemoveListener(OnMenu);
        }

        private void OnRestart()
        {
            OnRestartSelected?.Invoke();
        }

        private void OnMenu()
        {
            OnMenuSelected?.Invoke();
        }
    }
}