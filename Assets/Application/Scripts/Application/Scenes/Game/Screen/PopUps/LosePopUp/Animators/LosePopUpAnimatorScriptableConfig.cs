using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.LosePopUp.Animators
{
    [CreateAssetMenu(fileName = "LosePopUpConfig", menuName = "PopUp/LoseAnimationConfig")]
    public class LosePopUpAnimatorScriptableConfig : ScriptableObject
    {
        [SerializeField] private LosePopUpAnimatorConfig animatorConfig;

        public LosePopUpAnimatorConfig AnimatorConfig => animatorConfig;
    }
}