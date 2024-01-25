using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//Данные игрового баланса, такие как ингридиенты, свойства ингридиентов, названия, описания
//Варочные котлы, и тд. Настройки сеток, жидкостей и тд.

namespace Global.Managers.Datas
{
    [CreateAssetMenu(fileName = "GameBalanceData", menuName = "Scriptables/Game balance data", order = 51)]
    public class GameBalanceData : ScriptableObject
    {
        [SerializeField] private AnimationCurve corruptionByLevelChance;
        [SerializeField] private AnimationCurve corruptionByLevelMultiplier;
        [SerializeField] private int corruptionMin;
        [SerializeField] private int corruptionMax;

        [SerializeField] private int corruptionAdditionValue;

        [SerializeField] private AnimationCurve salaryByLevel;

        [SerializeField] private AnimationCurve maxDemocracyByLevel;
        [SerializeField] private AnimationCurve maxCorruptionByLevel;

        [SerializeField] private AnimationCurve leveling;
        [SerializeField] private AnimationCurve problemMultiplier;

        public int eventChance;

        #region problems

        public List<ProblemStaticData> problems;
        public List<ShopPositionData> shopItems;
        public float ProblemMultiplier => problemMultiplier.Evaluate(Level);

        #endregion

        private HomeStageData mainData => Services.GetManager<DataManager>().DynamicData.GameData.homeData;

        private int Level => mainData.level;
        private int Democracy => mainData.democracy;
        private int Corruption => mainData.corruption;

        public int MaxDemocracy => (int)maxDemocracyByLevel.Evaluate(Level);
        public int MaxCorruption => (int)maxCorruptionByLevel.Evaluate(Level);
        public int Salary => (int)salaryByLevel.Evaluate(Level);
        public int LevelUpgrade => (int)leveling.Evaluate(Level);

        public void Init()
        {
        }

        public float DemocracyPercent => Democracy / maxDemocracyByLevel.Evaluate(Level);
        public float CorruptionPercent => Corruption / maxCorruptionByLevel.Evaluate(Level);

        public int GetNewMoneyCorruption()
        {
            if (Random.Range(0, 100) > corruptionByLevelChance.Evaluate(Level) + corruptionAdditionValue * CorruptionPercent)
            {
                return 0;
            }
            return (int)(corruptionByLevelMultiplier.Evaluate(Level) * (Random.Range(corruptionMin, corruptionMax) + Random.Range(0, Corruption)));
        }

        public int GetNewEventID()
        {
            int sum = problems.Sum(x => x.valueRandAppear);
            int rand = Random.Range(0, sum);
            sum = 0;
            for (int i = 0; i < problems.Count; i++)
            {
                sum += problems[i].valueRandAppear;
                if (rand <= sum)
                {
                    return i;
                }
            }
            return 0;
        }
    }

}
