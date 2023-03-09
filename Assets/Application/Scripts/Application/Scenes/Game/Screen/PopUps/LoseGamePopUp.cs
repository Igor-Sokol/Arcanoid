using System;
using Application.Scripts.Application.Scenes.Shared.UI.EnergyViews;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps
{
    public class LoseGamePopUp : PopUp
    {
        [SerializeField] private Button addHealthButton;
        [SerializeField] private EnergyPriceView addHealthPrice;
        [SerializeField] private Button restartButton;
        [SerializeField] private EnergyPriceView restartPrice;
        [SerializeField] private Button menuButton;

        public override bool Active => gameObject.activeSelf;
        public override IPopUpAnimator PopUpAnimator { get; set; }
        public bool AddHealthActive { get => addHealthButton.interactable; set => addHealthButton.interactable = value; }
        public EnergyPriceView HealthPrice => addHealthPrice;
        public bool RestartActive { get => restartButton.interactable; set => restartButton.interactable = value; }
        public EnergyPriceView RestartPrice => restartPrice;
        public bool MenuActive { get => menuButton.interactable; set => menuButton.interactable = value; }
        public override event Action OnShown;
        public override event Action OnHidden;

        public event Action OnAddHealthSelected;
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
            OnAddHealthSelected = null;
            OnRestartSelected = null;
            OnMenuSelected = null;
        }

        private void OnEnable()
        {
            addHealthButton.onClick.AddListener(OnAddHealth);
            restartButton.onClick.AddListener(OnRestart);
            menuButton.onClick.AddListener(OnMenu);
        }
        private void OnDisable()
        {
            addHealthButton.onClick.RemoveListener(OnAddHealth);
            restartButton.onClick.RemoveListener(OnRestart);
            menuButton.onClick.RemoveListener(OnMenu);
        }

        private void OnAddHealth()
        {
            OnAddHealthSelected?.Invoke();
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