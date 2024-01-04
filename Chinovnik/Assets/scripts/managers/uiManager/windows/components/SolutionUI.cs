using System;
using Global;
using Global.Managers.Datas;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class SolutionUI : RectComponent
{
    [SerializeField] private Text description;
    [SerializeField] private Text textCost;
    [SerializeField] private Text textMoneyCost;
    [SerializeField] private Text pointsMinMax;
    [SerializeField] private Button button;

    [SerializeField] private bool isCorruption;

    private float multiplier => Services.GetManager<DataManager>().StaticData.Balance.ProblemMultiplier;

    public void Set(ProblemSolveVariant solveVariant, bool enable)
    {
        description.text = solveVariant.text;
        textCost.text = ((int)(solveVariant.cost * (isCorruption ? multiplier : 1))).ToString();
        textMoneyCost.text = ((int)(solveVariant.moneyCost * multiplier)).ToString();
        pointsMinMax.text = (int)(solveVariant.pointsMin * multiplier) + "-" + (int)(solveVariant.pointsMax * multiplier);
        button.interactable = enable;
    }

}
