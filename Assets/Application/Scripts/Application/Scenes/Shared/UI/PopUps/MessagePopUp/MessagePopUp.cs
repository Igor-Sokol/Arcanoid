using System;
using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Shared.UI.PopUps.MessagePopUp
{
    public class MessagePopUp : PopUp
    {
        [SerializeField] private MessagePopUpAnimator messagePopUpAnimator;
        [SerializeField] private Button continueButton;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image noninteractive;
        
        public override bool Active => gameObject.activeSelf;

        public override event Action OnShown;
        public override event Action OnHidden;
        public event Action OnContinueSelected;

        public void Configure(string message)
        {
            text.text = message;
        }
        
        protected override void ShowAction()
        {
            gameObject.SetActive(true);
            noninteractive.enabled = false;
            messagePopUpAnimator.ShowAnimation();
        }

        protected override void HideAction()
        {
            noninteractive.enabled = true;
            messagePopUpAnimator.HideAnimation();
        }
        
        private void OnEnable()
        {
            continueButton.onClick.AddListener(ContinueSelected);
            messagePopUpAnimator.OnAnimationShown += Shown;
            messagePopUpAnimator.OnAnimationHidden += Hidden;
        }

        private void OnDisable()
        {
            continueButton.onClick.RemoveListener(ContinueSelected);
            messagePopUpAnimator.OnAnimationShown -= Shown;
            messagePopUpAnimator.OnAnimationHidden -= Hidden;
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
        }
        
        private void ContinueSelected() => OnContinueSelected?.Invoke();
    }
}