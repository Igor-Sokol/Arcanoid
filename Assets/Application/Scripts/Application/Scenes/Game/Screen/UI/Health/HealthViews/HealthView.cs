using Application.Scripts.Library.Reusable;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.UI.Health.HealthViews
{
    public class HealthView : MonoBehaviour, IReusable
    {
        private bool _enabled;
        
        [SerializeField] private Image image;
        [SerializeField] private Color enableColor;
        [SerializeField] private Color disableColor;

        public Sprite Sprite => image.sprite;
        public bool Enabled => _enabled;

        public void Enable()
        {
            _enabled = true;
            image.color = enableColor;
        }

        public void Disable()
        {
            _enabled = false;
            image.color = disableColor;
        }

        public void PrepareReuse()
        {
            Disable();
        }
    }
}