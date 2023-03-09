using Application.Scripts.Application.Scenes.Shared.UI.StringViews;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.UI.LevelPackViews
{
    public class LevelPackView : MonoBehaviour
    {
        [SerializeField] private ProgressView packProgress;
        [SerializeField] private ProgressView levelProgress;
        [SerializeField] private Image packImage;

        public ProgressView PackProgress => packProgress;
        public ProgressView LevelProgress => levelProgress;
        public Sprite PackImage { get => packImage.sprite; set => packImage.sprite = value; }
    }
}