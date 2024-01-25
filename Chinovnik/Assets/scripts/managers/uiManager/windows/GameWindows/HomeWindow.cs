using System;
using System.Collections;
using System.Collections.Generic;
using Global.Components.UserInterface;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Global.Managers.Game
{
    using Datas;

    [NamedBehavior]
    public class HomeWindow : BaseCloseableWindow
    {
        [SerializeField] private Button yesStamp;
        [SerializeField] private Button noStamp;
        [SerializeField] private Text level;
        [SerializeField] private Text docTotalCount;
        [SerializeField] private Bar democracyBar;
        [SerializeField] private Bar corruptionBar;
        [SerializeField] private Text moneyText;
        [SerializeField] private Document document;
        [SerializeField] private Text advice;
        [SerializeField] private Text singText;
        [SerializeField] private Text singObject;

        [SerializeField] private GameObject newLevelWindow;
        [SerializeField] private GameObject eventWin;
        [SerializeField] private GameObject eventLose;
        [SerializeField] private Text eventLoseLevel;
        [SerializeField] private Text eventLoseMoney;

        [SerializeField] private List<ShoppingItem> shoppingItems;

        protected override Type WindowType => typeof(HomeWindow);

        private DataManager data => Services.GetManager<DataManager>();
        private GameBalanceData balance => data.StaticData.Balance;
        private HomeStageData game => data.DynamicData.GameData.homeData;

        private bool currentLawDecision;
        private int currentCorruption;

        protected override void OnHide()
        {
        }

        protected override void OnShow()
        {
            foreach (ShoppingItem item in shoppingItems)
            {
                item.SetFromData();
            }

            SetValuesFromData();
            if (Services.GetManager<GameManager>().CurrentGame.currentStageData == GameData.GameStage.home)
            {
                StartCoroutine(MainLifeCycle());
            }
        }

        public void PressYes()
        {
            StartCoroutine(ButtonPressCycle(true));
        }

        public void PressNo()
        {
            StartCoroutine(ButtonPressCycle(false));
        }

        public void LockButtons()
        {
            yesStamp.interactable = false;
            noStamp.interactable = false;
        }

        public void UnlockButtons()
        {
            yesStamp.interactable = true;
            noStamp.interactable = true;
        }

        private IEnumerator MainLifeCycle()
        {
            LockButtons();
            if (game.documentsTotal > 50 - game.level * 5 && Random.Range(0, 100) < balance.eventChance)
            {
                Services.GetManager<GameManager>().GotoStage(GameData.GameStage.problem);
                yield break;
            }
            currentCorruption = balance.GetNewMoneyCorruption();
            document.ShowNew(currentCorruption);
            currentLawDecision = Random.Range(0, 100) > 50;
            advice.text = "По закону вы должны поставить " + (currentLawDecision ? "YES" : "NO");
            singText.text = "Чиновник " + game.level + " звания,\nОтдел подписания важных документов\n\n_________________";
            yield return new WaitForSeconds(0.7f);
            UnlockButtons();
        }

        private IEnumerator ButtonPressCycle(bool decision)
        {
            LockButtons();
            document.SetDecision(decision);
            game.documentsTotal++;

            if (decision == currentLawDecision)
            {
                game.money += balance.Salary;
                game.democracy = Mathf.Clamp(game.democracy + 1, 0, balance.MaxDemocracy);
                if (currentCorruption > 0)
                {
                    game.corruption = Mathf.Clamp(game.corruption - 1, 0, game.corruption);
                    game.democracy = Mathf.Clamp(game.democracy + 2 + (int)(game.democracy * 0.1f) + (int)(balance.MaxDemocracy * 0.02f), 0, balance.MaxDemocracy);
                }
            }
            else
            {
                game.democracy = (int)Mathf.Clamp(game.democracy * 0.9f - 1, 0, game.democracy);
            }

            if (currentCorruption > 0 && decision != currentLawDecision)
            {
                game.money += currentCorruption;
                game.corruption = Mathf.Clamp(game.corruption + 1, 0, balance.MaxCorruption);
            }


            if (game.documentsTotal > balance.LevelUpgrade)
            {
                game.level++;
                game.documentsTotal = 0;
                yield return new WaitForSeconds(0.1f);
                newLevelWindow.SetActive(true);
            }

            advice.text = "";

            yield return new WaitForSeconds(0.1f);
            document.Hide();

            yield return new WaitForSeconds(0.6f);
            SetValuesFromData();
            StartCoroutine(MainLifeCycle());
        }

        private void SetValuesFromData()
        {
            level.text = game.level.ToString();
            docTotalCount.text = game.documentsTotal.ToString();
            democracyBar.Percent = balance.DemocracyPercent;
            democracyBar.Text = (int)(balance.DemocracyPercent * 100) + "%";
            corruptionBar.Percent = balance.CorruptionPercent;
            corruptionBar.Text = game.corruption.ToString();
            moneyText.text = game.money.ToString();
        }

        public void EventLog(bool result)
        {
            if (result)
            {
                eventWin.SetActive(true);
            }
            else
            {
                eventLose.SetActive(true);
                eventLoseLevel.text = "-" + balance.problems[data.DynamicData.GameData.problemData.problemIndex].penaltyLevel;
                eventLoseMoney.text = "-" + (int)(balance.problems[data.DynamicData.GameData.problemData.problemIndex].penaltyMoney * balance.ProblemMultiplier);
            }
        }
    }
}
