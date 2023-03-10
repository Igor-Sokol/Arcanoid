using System;
using Application.Scripts.Application.Scenes.Shared.UI.EnergyViews;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.LosePopUp
{
    public class LoseGamePopUp : PopUp
    {
        [SerializeField] private Button addHealthButton;
        [SerializeField] private EnergyPriceView addHealthPrice;
        [SerializeField] private Button restartButton;
        [SerializeField] private EnergyPriceView restartPrice;
        [SerializeField] private Button menuButton;

        public override bool Active => gameObject.activeSelf;
        public bool AddHealthActive { get => addHealthButton.interactable; set => addHealthButton.interactable = value; }
        public EnergyPriceView HealthPrice => addHealthPrice;
        public bool RestartActive { get => restartButton.interactable; set => restartButton.interactable = value; }
        public EnergyPriceView RestartPrice => restartPrice;
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
            addHealthButton.onClick.AddListener(AddHealthSelected);
            restartButton.onClick.AddListener(RestartSelected);
            menuButton.onClick.AddListener(MenuSelected);
        }
        private void OnDisable()
        {
            addHealthButton.onClick.RemoveListener(AddHealthSelected);
            restartButton.onClick.RemoveListener(RestartSelected);
            menuButton.onClick.RemoveListener(MenuSelected);
        }
        
        private void AddHealthSelected() => OnAddHealthSelected?.Invoke();
        private void RestartSelected() => OnRestartSelected?.Invoke();
        private void MenuSelected() => OnMenuSelected?.Invoke();
    }
}