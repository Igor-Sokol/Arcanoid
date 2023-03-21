using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps.Animators.WinAnimator
{
    [CreateAssetMenu(fileName = "WinPopUpConfig", menuName = "PopUp/WinAnimationConfig")]
    public class WinPopUpAnimatorScriptableConfig : ScriptableObject
    {
        [SerializeField] private WinPopUpAnimatorConfig animatorConfig;

        public WinPopUpAnimatorConfig AnimatorConfig => animatorConfig;
    }
}