using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Game
{
    using Datas;
    using UserInterface;

    public static class GameSetter
    {
        private static Dictionary<GameData.GameStage, Action<GameData>> gameSetterFromLoading = new Dictionary<GameData.GameStage, Action<GameData>>()
        {
            { GameData.GameStage.home, OnStageHomeLoad },
            { GameData.GameStage.problem, OnStageProblemLoad }
        };

        public static void SetGameFrom(GameData gameData)
        {
            gameSetterFromLoading[gameData.currentStageData](gameData);
        }

        #region setting functions

        private static void OnStageHomeLoad(GameData gameData)
        {
            Services.GetManager<UIManager>().HideWindow<ProblemWindow>();
            Services.GetManager<UIManager>().ShowWindow<HomeWindow>();
        }

        private static void OnStageProblemLoad(GameData gameData)
        {
            Services.GetManager<UIManager>().ShowWindow<HomeWindow>();
            Services.GetManager<UIManager>().ShowWindow<ProblemWindow>();
        }

        #endregion
    }
}
