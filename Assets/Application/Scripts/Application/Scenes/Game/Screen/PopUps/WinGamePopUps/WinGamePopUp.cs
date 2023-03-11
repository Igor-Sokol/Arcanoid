using System;
using Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps.Animators;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using Application.Scripts.Application.Scenes.Shared.UI.EnergyViews;
using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps
{
    public class WinGamePopUp : PopUp
    {
        [SerializeField] private WinPopUpAnimator winPopUpAnimator;
        [SerializeField] private Button continueButton;
        [SerializeField] private EnergyPriceView continuePrice;
        [SerializeField] private Button menuButton;
        [SerializeField] private EnergyView energyView;
        [SerializeField] private Image noninteractive;
        
        public override bool Active => gameObject.activeSelf;
        public bool ContinueActive { get => continueButton.interactable; set => continueButton.interactable = value; }
        public EnergyPriceView ContinuePrice => continuePrice;
        public WinPopUpAnimator WinPopUpAnimator => winPopUpAnimator;
        public EnergyView EnergyView => energyView;
        public override event Action OnShown;
        public override event Action OnHidden;
        public event Action OnContinueSelected;
        public event Action OnMenuSelected;

        protected override void ShowAction()
        {
            gameObject.SetActive(true);
            noninteractive.enabled = false;
            winPopUpAnimator.ShowAnimation();
        }

        protected override void HideAction()
        {
            noninteractive.enabled = true;
            winPopUpAnimator.HideAnimation();
        }

        private void OnEnable()
        {
            continueButton.onClick.AddListener(ContinueSelected);
            menuButton.onClick.AddListener(MenuSelected);
            winPopUpAnimator.OnAnimationShown += Shown;
            winPopUpAnimator.OnAnimationHidden += Hidden;
        }

        private void OnDisable()
        {
            continueButton.onClick.RemoveListener(ContinueSelected);
            menuButton.onClick.RemoveListener(MenuSelected);
            winPopUpAnimator.OnAnimationShown -= Shown;
            winPopUpAnimator.OnAnimationHidden -= Hidden;
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
            OnContinueSelected = null;
            OnMenuSelected = null;
        }
        private void ContinueSelected() => OnContinueSelected?.Invoke();
        private void MenuSelected() => OnMenuSelected?.Invoke();
    }
}