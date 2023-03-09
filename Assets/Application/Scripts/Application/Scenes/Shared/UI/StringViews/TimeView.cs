using System;
using TMPro;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.StringViews
{
    public class TimeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string format;
        [SerializeField] private char[] mask;

        public void SetTime(float seconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
            text.text = timeSpan.ToString(format).TrimStart(mask);
        }
    }
}