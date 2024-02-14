using System;
using System.Collections.Generic;
using System.Linq;
using Global.Components.UserInterface;
using Global.Managers.Datas;
using Global.Managers.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Managers.Game
{
    public class ShopWindow : BaseCloseableWindow
    {
        protected override Type WindowType => typeof(ShopWindow);

        [SerializeField] private Text moneyText;
        [SerializeField] private GameObject contentParent;
        [SerializeField] private ShopItemUI prefab;

        [SerializeField] private List<ShoppingItem> shoppingItems;

        private DataManager data => Services.GetManager<DataManager>();
        private HomeStageData game => data.DynamicData.GameData.homeData;

        private GameBalanceData balance => data.StaticData.Balance;

        private List<ShopItemUI> content = new List<ShopItemUI>();

        protected override void OnHide()
        {
        }

        protected override void OnShow()
        {
            UpdateShoppingItems();
            UpdateData();
        }

        public void UpdateShoppingItems()
        {
            foreach (ShoppingItem item in shoppingItems)
            {
                item.SetFromData();
            }
        }

        private void UpdateData()
        {
            moneyText.text = game.money.ToString();
            SetShopArea();
        }

        public void SetShopArea()
        {
            int counter = 0;
            for (int i = 0; i < balance.shopItems.Count; i++)
            {
                ShopPositionData shopPositionData = balance.shopItems[i];
                for (int j = 0; j < shopPositionData.items.Count; j++)
                {
                    ShopItemData item = shopPositionData.items[j];
                    if (content.Count <= counter)
                    {
                        ShopItemUI itemUI = Instantiate(prefab, contentParent.transform);
                        content.Add(itemUI);
                    }
                    content[counter].SetData(item, shopPositionData, OnBuy, OnSelect);
                    counter++;
                }
            }
        }

        public void OnBuy(int id, int innerID)
        {
            ShopItemData item = balance.shopItems[id].items[innerID];

            //buy item
            game.purchased[id].data[innerID] = true;
            game.money -= item.cost;

            //select?
            game.selectedShopItems[id] = innerID;

            //update
            content.First(x => x.ID == id && x.InnerID == innerID).UpdateData(item, balance.shopItems[id]);

            //update ShoppingItems
            UpdateShoppingItems();
            UpdateData();
            Services.GetManager<UIManager>().GetWindow<HomeWindow>().UpdateShoppingItems();
        }

        public void OnSelect(int id, int innerID)
        {
            ShopItemData item = balance.shopItems[id].items[innerID];

            //select
            game.selectedShopItems[id] = innerID;

            //update
            content.First(x => x.ID == id && x.InnerID == innerID).UpdateData(item, balance.shopItems[id]);

            UpdateShoppingItems();
            UpdateData();
            Services.GetManager<UIManager>().GetWindow<HomeWindow>().UpdateShoppingItems();
        }

    }

}
