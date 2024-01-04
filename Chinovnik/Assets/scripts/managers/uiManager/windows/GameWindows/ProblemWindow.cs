using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Global;
using Global.Components.UserInterface;
using Global.Managers.Datas;
using Global.Managers.Game;
using Global.Managers.UserInterface;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[NamedBehavior]
public class ProblemWindow : BaseCloseableWindow
{
    [SerializeField] private Text mainProblemText;
    [SerializeField] private Text pointsTextCount;

    [SerializeField] private List<SolutionUI> solutionsDemocracy;
    [SerializeField] private List<SolutionUI> solutionsCorruption;

    protected override Type WindowType => typeof(ProblemWindow);

    private ProblemStageData data => Services.GetManager<DataManager>().DynamicData.GameData.problemData;
    private HomeStageData homeData => Services.GetManager<DataManager>().DynamicData.GameData.homeData;

    private GameBalanceData balance => Services.GetManager<DataManager>().StaticData.Balance;

    private float multiplier => Services.GetManager<DataManager>().StaticData.Balance.ProblemMultiplier;

    protected override void OnHide()
    {

    }

    protected override void OnShow()
    {
        GenerateRandomEvent();
        SetFromData();

        //wait until buttons click
    }

    private void SetFromData()
    {
        mainProblemText.text = balance.problems[data.problemIndex].mainText;
        pointsTextCount.text = data.basePoints + "/" + data.pointsNeeds;
        int democracyPercent = (int)(100 * balance.DemocracyPercent);
        int corruption = homeData.corruption;
        for (int i = 0; i < solutionsDemocracy.Count; i++)
        {
            ProblemSolveVariant v = balance.problems[data.problemIndex].democracySolve[i];
            solutionsDemocracy[i].Set(v, v.cost <= democracyPercent && homeData.money >= v.moneyCost * multiplier);
        }
        for (int i = 0; i < solutionsCorruption.Count; i++)
        {
            ProblemSolveVariant v = balance.problems[data.problemIndex].corruptionSolve[i];
            solutionsCorruption[i].Set(v, v.cost * multiplier <= corruption && homeData.money >= v.moneyCost * multiplier);
        }
    }

    private void GenerateRandomEvent()
    {
        int id = balance.GetNewEventID();
        data.problemIndex = id;
        data.pointsNeeds = (int)(balance.problems[id].pointsNeeds * balance.ProblemMultiplier);
        data.basePoints = (int)(homeData.democracy + Mathf.Pow(homeData.corruption, 1.3f));
    }

    public void DoDemocracy(int index)
    {
        ProblemSolveVariant v = balance.problems[data.problemIndex].democracySolve[index];
        data.basePoints += (int)(Random.Range(v.pointsMin, v.pointsMax) * multiplier);
        homeData.money -= (int)(v.moneyCost * multiplier);
        if (homeData.money < 0)
        {
            homeData.money = 0;
        }
        DoNothing();
    }

    public void DoCorruption(int index)
    {
        ProblemSolveVariant v = balance.problems[data.problemIndex].corruptionSolve[index];
        data.basePoints += (int)(Random.Range(v.pointsMin, v.pointsMax) * multiplier);
        homeData.money -= (int)(v.moneyCost * multiplier);
        if (homeData.money < 0)
        {
            homeData.money = 0;
        }
        DoNothing();
    }

    public void DoNothing()
    {
        if (data.basePoints >= data.pointsNeeds)
        {
            Services.GetManager<UIManager>().HideWindow<ProblemWindow>();
            Services.GetManager<GameManager>().GotoStage(GameData.GameStage.home);
        }
        else
        {
            homeData.level = Mathf.Clamp(homeData.level - balance.problems[data.problemIndex].penaltyLevel, 0, homeData.level);
            homeData.money -= balance.problems[data.problemIndex].penaltyMoney;
            if (homeData.money < 0)
            {
                homeData.money = 0;
            }
            Services.GetManager<GameManager>().GotoStage(GameData.GameStage.home);
        }
    }

}
