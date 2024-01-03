using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Components.UserInterface
{
    using Tools;
    using Managers.Datas;

    [Assert]
    public class SettingsWindow : BaseCloseableWindow
    {
        protected override Type WindowType => typeof(SettingsWindow);

#pragma warning disable
#pragma warning restore

        protected override void OnHide()
        {
        }

        protected override void OnShow()
        {
        }
    }
}
