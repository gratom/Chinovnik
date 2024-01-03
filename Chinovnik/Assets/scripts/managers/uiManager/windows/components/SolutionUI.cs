using System;
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

    private int index;

    public void Set(ProblemSolveVariant solveVariant, int index)
    {
        description.text = solveVariant.text;
        textCost.gameObject.SetActive(solveVariant.cost > 0);
        textCost.text = solveVariant.cost > 0 ? solveVariant.ToString() : "";
        textMoneyCost.gameObject.SetActive(solveVariant.moneyCost > 0);
        textMoneyCost.text = solveVariant.moneyCost > 0 ? solveVariant.ToString() : "";
        pointsMinMax.text = solveVariant.pointsMin + "-" + solveVariant.pointsMax;
    }

}
