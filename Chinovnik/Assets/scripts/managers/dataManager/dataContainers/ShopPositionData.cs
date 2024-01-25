using System;
using System.Collections.Generic;

namespace Global.Managers.Datas
{
    [Serializable]
    public class ShopPositionData
    {
        public string description;
        public List<ShopItemData> items;
    }
}
