using System;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using Application.Scripts.Application.Scenes.Shared.UI.EnergyViews;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using Application.Scripts.Library.Reusable;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps
{
    public class WinGamePopUp : PopUp, IReusable
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private EnergyPriceView continuePrice;
        [SerializeField] private Button menuButton;
        [SerializeField] private WinGamePopUpView popUpView;
        
        public override bool Active => gameObject.activeSelf;
        public override IPopUpAnimator PopUpAnimator { get; set; }
        public bool MenuActive { get => menuButton.interactable; set => menuButton.interactable = value; }
        public bool ContinueActive { get => continueButton.interactable; set => continueButton.interactable = value; }
        public EnergyPriceView ContinuePrice => continuePrice;
        public override event Action OnShown;
        public override event Action OnHidden;
        public event Action OnContinueSelected;
        public event Action OnMenuSelected;

        public void Configure(IPackInfo packInfo)
        {
            popUpView.Configure(packInfo);
        }
        
        public void PrepareReuse()
        {
            menuButton.interactable = true;
            continueButton.interactable = true;
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
            OnContinueSelected = null;
            OnMenuSelected = null;
        }

        private void OnEnable()
        {
            continueButton.onClick.AddListener(ContinueSelected);
            menuButton.onClick.AddListener(OnMenu);
        }

        private void OnDisable()
        {
            continueButton.onClick.RemoveListener(ContinueSelected);
            menuButton.onClick.RemoveListener(OnMenu);
        }
        
        private void ContinueSelected()
        {
            OnContinueSelected?.Invoke();
        }
        
        private void OnMenu()
        {
            OnMenuSelected?.Invoke();
        }
    }
}