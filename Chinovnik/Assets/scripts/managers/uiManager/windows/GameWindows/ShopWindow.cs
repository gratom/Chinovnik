using System;
using Global.Components.UserInterface;
using Global.Managers.Datas;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Managers.Game
{
    public class ShopWindow : BaseCloseableWindow
    {
        protected override Type WindowType => typeof(ShopWindow);

        [SerializeField] private Text moneyText;
        [SerializeField] private GameObject content;
        [SerializeField] private ShopItemUI prefab;

        private DataManager data => Services.GetManager<DataManager>();
        private HomeStageData game => data.DynamicData.GameData.homeData;

        protected override void OnHide()
        {
        }

        protected override void OnShow()
        {
            UpdateData();
        }

        private void UpdateData()
        {
            moneyText.text = game.money.ToString();
        }

        public void SetShopArea()
        {

        }


    }

}
