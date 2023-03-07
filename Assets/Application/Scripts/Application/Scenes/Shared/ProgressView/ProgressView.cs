using TMPro;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.ProgressView
{
    public class ProgressView : MonoBehaviour
    {
        [SerializeField] private TMP_Text progressText;
        [SerializeField] private string mask;

        public void SetProgress(int current, int max)
        {
            progressText.text = string.Format(mask, current, max);
        }
    }
}