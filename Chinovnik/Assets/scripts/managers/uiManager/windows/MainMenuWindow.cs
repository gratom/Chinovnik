using System.Collections;
using System.Collections.Generic;
using Global.Managers;
using Global.Managers.UserInterface;
using UnityEngine;

namespace Global.Components.UserInterface
{
    public class MainMenuWindow : BaseWindow
    {
        protected override void OnHide()
        {
        }

        protected override void OnShow()
        {
        }

        #region buttons

        public void NewGameClick()
        {
            Services.GetManager<MainManager>().StartNewGame();
        }

        public void ContinueGameClick()
        {
            Services.GetManager<MainManager>().ContinueGame();
        }

        public void SettingsClick()
        {
            Services.GetManager<UIManager>().ShowWindow<SettingsWindow>();
        }

        public void AboutClick()
        {
            Services.GetManager<UIManager>().ShowWindow<AboutWindow>();
        }

        #endregion
    }
}
