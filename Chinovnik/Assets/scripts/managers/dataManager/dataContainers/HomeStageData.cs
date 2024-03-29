﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Managers.Datas
{
    [Serializable]
    public class HomeStageData : BaseStageData
    {
        public override GameData.GameStage Stage => GameData.GameStage.home;

        public long money;
        public int level = 1;
        public int democracy = 1;
        public int corruption = 1;
        public int documentsTotal;

        public int[] selectedShopItems;

        public List<PurchasedData> purchased;

        #region public functions

        public void PostInit()
        {

        }

        #endregion
    }

    [Serializable]
    public class PurchasedData
    {
        public bool[] data;
    }
}
