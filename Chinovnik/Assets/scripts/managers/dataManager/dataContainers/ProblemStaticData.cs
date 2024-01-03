using System;
using System.Collections.Generic;

namespace Global.Managers.Datas
{
    [Serializable]
    public class ProblemStaticData
    {
        public int valueRandAppear;
        public string mainText;
        public List<ProblemSolveVariant> democracySolve;
        public List<ProblemSolveVariant> corruptionSolve;
        public int pointsNeeds;
        public int penaltyLevel;
        public int penaltyMoney;
    }

    [Serializable]
    public class ProblemSolveVariant
    {
        public string text;
        public int cost;
        public int moneyCost;
        public int pointsMin;
        public int pointsMax;
    }
}
