using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Global.Managers
{
    using Datas;
    using Game;

    public class MainManager : BaseManager
    {
        public override Type ManagerType => typeof(MainManager);

#pragma warning disable

        protected override async Task<bool> OnInit()
        {
            return true;
        }

#pragma warning restore

        public void EntryPoint()
        {
            Services.GetManager<DataManager>().StaticData.Balance.Init();
            ContinueGame();
        }

        public void StartNewGame()
        {
            //ContinueGame();
        }

        public void ContinueGame()
        {
            if (Services.GetManager<DataManager>().DynamicData.GameData == null)
            {
                Debug.Log("data is null. create new");
                Services.GetManager<DataManager>().DynamicData.GameData = Services.GetManager<DataManager>().StaticData.DefaultGameData.GetCopyOfData();
            }
            Services.GetManager<DataManager>().DynamicData.GameData.PostInitData();
            Services.GetManager<GameManager>().StartGame(Services.GetManager<DataManager>().DynamicData.GameData);
        }
    }
}
