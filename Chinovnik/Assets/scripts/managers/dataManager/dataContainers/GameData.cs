using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    [Serializable]
    public class GameData
    {
        public bool isInited = false;

        public HomeStageData homeData = new HomeStageData();
        public ProblemStageData problemData = new ProblemStageData();

        public enum GameStage
        {
            home,
            problem
        }

        public GameStage currentStageData = GameStage.home;

        public void PostInitData()
        {
            homeData.PostInit();
        }
    }
}
