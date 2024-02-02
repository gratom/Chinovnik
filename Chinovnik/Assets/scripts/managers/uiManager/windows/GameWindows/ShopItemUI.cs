using System;
using Global.Managers.Datas;
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

        [SerializeField] private Button button;

        [SerializeField] private int id;
        [SerializeField] private int innerID;
        [SerializeField] private bool isPurchased;
        [SerializeField] private bool isSelected;

        public int ID => id;
        public int InnerID => innerID;

        private DataManager data => Services.GetManager<DataManager>();
        private HomeStageData game => data.DynamicData.GameData.homeData;

        private Action<int, int> buyAction;
        private Action<int, int> selectAction;

        public void OnClick()
        {
            if (!isPurchased)
            {
                Buy();
            }
            else
            {
                Select();
            }
        }

        private void Select()
        {
            selectAction(id, innerID);
        }

        private void Buy()
        {
            buyAction(id, innerID);
        }

        public void UpdateData(ShopItemData item, ShopPositionData shopPositionData)
        {
            isPurchased = game.purchased[shopPositionData.ID].data[item.InnerID];
            isSelected = game.selectedShopItems[shopPositionData.ID] == item.InnerID;

            if (!isPurchased)
            {
                costText.text = item.cost + "$";
                button.interactable = item.cost <= game.money;
            }
            else
            {
                costText.text = isSelected ? "Выбрано" : "Выбрать";
                button.interactable = !isSelected;
            }
        }

        public void SetData(ShopItemData item, ShopPositionData shopPositionData, Action<int, int> onBuy, Action<int, int> onSelect)
        {
            buyAction = onBuy;
            selectAction = onSelect;

            id = shopPositionData.ID;
            innerID = item.InnerID;
            mainImage.sprite = item.sprite;
            isPurchased = game.purchased[shopPositionData.ID].data[item.InnerID];
            isSelected = game.selectedShopItems[shopPositionData.ID] == item.InnerID;

            if (!isPurchased)
            {
                costText.text = item.cost + "$";
                button.interactable = item.cost <= game.money;
            }
            else
            {
                costText.text = isSelected ? "Выбрано" : "Выбрать";
                button.interactable = !isSelected;
            }

            descriptionText.text = item.description;
        }
    }
}
