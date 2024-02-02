using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using Global.Managers.Datas;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingItem : MonoBehaviour
{
    [SerializeField] private Image shopItem;
    [SerializeField] private int id;

    private HomeStageData data => Services.GetManager<DataManager>().DynamicData.GameData.homeData;
    private GameBalanceData balance => Services.GetManager<DataManager>().StaticData.Balance;

    private void OnEnable()
    {
        SetFromData();
    }

    public void SetFromData()
    {
        //shopItem.enabled = data.selectedShopItems[id] != 0;
        int dataID = data.selectedShopItems[id];
        shopItem.sprite = balance.shopItems[id].items[dataID].sprite;
    }
}
