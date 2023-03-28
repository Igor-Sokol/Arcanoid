using Application.Scripts.Application.Scenes.Shared.UI.StringViews;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.UI.EnergyViews
{
    public class EnergyPriceView : MonoBehaviour
    {
        [SerializeField] private TextValue priceText;

        public void SetPrice(int price)
        {
            priceText.SetValue(price);
        }
    }
}