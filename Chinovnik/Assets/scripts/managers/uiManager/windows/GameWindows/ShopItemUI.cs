using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Managers.Game
{
    public class ShopItemUI : RectComponent
    {
        [SerializeField] private Image mainImage;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Text costText;

        [SerializeField] private Toggle selected;
        [SerializeField] private Button buttonBuy;

        public void SetData(Sprite mainSprite, int cost, string description, bool isSelected, bool isPurchased)
        {
            buttonBuy.interactable = isPurchased;
            mainImage.sprite = mainSprite;
            selected.isOn = isSelected;
            costText.text = cost.ToString();
            descriptionText.text = description;
        }

    }
}
