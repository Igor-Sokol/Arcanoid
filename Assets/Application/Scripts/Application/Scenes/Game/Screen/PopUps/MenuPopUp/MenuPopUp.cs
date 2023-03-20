using System;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps.MenuPopUp.Animators;
using Application.Scripts.Application.Scenes.Shared.UI.EnergyViews;
using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.MenuPopUp
{
    public class MenuPopUp : PopUp
    {
        [SerializeField] private MenuPopUpAnimator menuPopUpAnimator;
        [SerializeField] private Button restartButton;
        [SerializeField] private EnergyPriceView restartPrice;
        [SerializeField] private Button backButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button skipButton;
        [SerializeField] private EnergyPriceView skipPrice;
        [SerializeField] private Image noninteractive;
        
        public override bool Active => gameObject.activeSelf;
        public bool RestartActive { get => restartButton.interactable; set => restartButton.interactable = value; }
        public EnergyPriceView RestartPrice => restartPrice;
        public bool SkipActive { get => skipButton.interactable; set => skipButton.interactable = value; }
        public EnergyPriceView SkipPrice => skipPrice;
        
        public override event Action OnShown;
        public override event Action OnHidden;
        public event Action OnRestartSelected;
        public event Action OnBackSelected;
        public event Action OnContinueSelected;
        public event Action OnSkipSelected;

        protected override void ShowAction()
        {
            gameObject.SetActive(true);
            noninteractive.enabled = false;
            menuPopUpAnimator.ShowAnimation();
        }

        protected override void HideAction()
        {
            noninteractive.enabled = true;
            menuPopUpAnimator.HideAnimation();
        }
        
        private void OnEnable()
        {
            restartButton.onClick.AddListener(RestartSelected);
            backButton.onClick.AddListener(BackSelected);
            continueButton.onClick.AddListener(ContinueSelected);
            skipButton.onClick.AddListener(SkipSelected);
            menuPopUpAnimator.OnAnimationShown += Shown;
            menuPopUpAnimator.OnAnimationHidden += Hidden;
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(RestartSelected);
            backButton.onClick.RemoveListener(BackSelected);
            continueButton.onClick.RemoveListener(ContinueSelected);
            skipButton.onClick.RemoveListener(SkipSelected);
            menuPopUpAnimator.OnAnimationShown -= Shown;
            menuPopUpAnimator.OnAnimationHidden -= Hidden;
        }

        private void Shown()
        {
            OnShown?.Invoke();
        }
        private void Hidden()
        { 
            gameObject.SetActive(false);
            OnHidden?.Invoke();

            OnHidden = null;
            OnShown = null;
            OnRestartSelected = null;
            OnBackSelected = null;
            OnContinueSelected = null;
        }
        private void RestartSelected() => OnRestartSelected?.Invoke();
        private void BackSelected() => OnBackSelected?.Invoke();
        private void ContinueSelected() => OnContinueSelected?.Invoke();
        private void SkipSelected() => OnSkipSelected?.Invoke();
    }
}