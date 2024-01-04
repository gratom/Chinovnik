using System;

namespace Global.Managers.Datas
{
    [Serializable]
    public class ProblemStageData : BaseStageData
    {
        public int problemIndex;
        public int pointsNeeds;
        public int basePoints;
        public override GameData.GameStage Stage => GameData.GameStage.problem;

        #region public functions

        public void PostInit()
        {

        }

        #endregion
    }
}
