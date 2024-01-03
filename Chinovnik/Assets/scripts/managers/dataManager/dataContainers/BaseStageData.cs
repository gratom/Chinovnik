using System;
using Global.Components.UserInterface;

namespace Global.Managers.Datas
{
    [Serializable]
    public abstract class BaseStageData
    {
        public abstract GameData.GameStage Stage { get; }
    }
}
