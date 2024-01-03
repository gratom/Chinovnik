using System;
using UnityEngine;

namespace Global.Components.UserInterface
{
    public class AboutWindow : BaseCloseableWindow
    {
        protected override Type WindowType => typeof(AboutWindow);

        public void ButtonClickOpenURL(string URL)
        {
            Application.OpenURL(URL);
        }

        protected override void OnHide()
        {
        }

        protected override void OnShow()
        {
        }
    }
}
