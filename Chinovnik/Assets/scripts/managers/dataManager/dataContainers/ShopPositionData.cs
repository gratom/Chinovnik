using System;
using System.Collections.Generic;

namespace Global.Managers.Datas
{
    [Serializable]
    public class ShopPositionData
    {
        public int ID;
        public string description;
        public List<ShopItemData> items;
    }
}
