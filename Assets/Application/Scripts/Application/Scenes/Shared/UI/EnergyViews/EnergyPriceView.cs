using TMPro;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.UI.EnergyViews
{
    public class EnergyPriceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text priceText;

        public void SetPrice(int price)
        {
            priceText.text = price.ToString();
        }
    }
}