using UnityEngine;

namespace Global.Managers.Datas
{
    [CreateAssetMenu(fileName = "StaticData", menuName = "Scriptables/Static data", order = 51)]
    public class StaticData : ScriptableObject
    {
#pragma warning disable

        [SerializeField] private string dynamicDataLocation = "dynamicData";
        [SerializeField] private GameDataScriptableWrapper defaultGameData;
        [SerializeField] private GameBalanceData gameBalanceData;

#pragma warning restore

        public string DynamicDataLocation => dynamicDataLocation;
        public GameDataScriptableWrapper DefaultGameData => defaultGameData;
        public GameBalanceData Balance => gameBalanceData;
    }
}
