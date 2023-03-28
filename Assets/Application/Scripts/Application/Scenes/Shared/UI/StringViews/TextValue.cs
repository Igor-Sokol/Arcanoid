using TMPro;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.UI.StringViews
{
    public class TextValue : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private string formatMask;

        public void SetValue(params object[] values)
        {
            formatMask ??= tmpText.text;

            tmpText.text = string.Format(formatMask, values);
        }
    }
}