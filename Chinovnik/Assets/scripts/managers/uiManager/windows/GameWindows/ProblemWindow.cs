using System;
using System.Collections;
using System.Collections.Generic;
using Global.Components.UserInterface;
using Tools;
using UnityEngine;
using UnityEngine.UI;

[NamedBehavior]
public class ProblemWindow : BaseCloseableWindow
{
    [SerializeField] private Text mainProblemText;
    [SerializeField] private Text pointsTextCount;

    [SerializeField] private List<SolutionUI> solutionsDemocracy;
    [SerializeField] private List<SolutionUI> solutionsCorruption;

    protected override Type WindowType => typeof(ProblemWindow);

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

    }

    private void GenerateRandomEvent()
    {

    }

    public void DoDemocracy(int index)
    {

    }

    public void DoCorruption(int index)
    {

    }

    public void DoNothing()
    {

    }

}
