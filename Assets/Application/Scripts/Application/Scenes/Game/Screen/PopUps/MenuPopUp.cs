using System;
using Application.Scripts.Application.Scenes.Shared.UI.EnergyViews;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps
{
    public class MenuPopUp : PopUp
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private EnergyPriceView restartPrice;
        [SerializeField] private Button backButton;
        [SerializeField] private Button continueButton;
        
        public override bool Active => gameObject.activeSelf;
        public override IPopUpAnimator PopUpAnimator { get; set; }
        public bool RestartActive { get => restartButton.interactable; set => restartButton.interactable = value; }
        public EnergyPriceView RestartPrice => restartPrice;
        public override event Action OnShown;
        public override event Action OnHidden;
        public event Action OnRestartSelected;
        public event Action OnBackSelected;
        public event Action OnContinueSelected;

        private void OnEnable()
        {
            restartButton.onClick.AddListener(RestartSelected);
            backButton.onClick.AddListener(BackSelected);
            continueButton.onClick.AddListener(ContinueSelected);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(RestartSelected);
            backButton.onClick.RemoveListener(BackSelected);
            continueButton.onClick.RemoveListener(ContinueSelected);
        }

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
            OnBackSelected = null;
            OnContinueSelected = null;
        }

        private void RestartSelected()
        {
            OnRestartSelected?.Invoke();
        }
        
        private void BackSelected()
        {
            OnBackSelected?.Invoke();
        }
        
        private void ContinueSelected()
        {
            OnContinueSelected?.Invoke();
        }
    }
}