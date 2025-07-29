using System;
using UnityEngine;
using System.Collections;
using System.IO;
using KSP.UI.Screens;
using ToolbarControl_NS;

namespace StateFunding
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class RegisterToolbar : MonoBehaviour
    {
        void Start()
        {
            ToolbarControl.RegisterMod(StateFundingApplicationLauncher.MODID, StateFundingApplicationLauncher.MODNAME);
        }
    }

    public class StateFundingApplicationLauncher: MonoBehaviour
    {

        private StateFundingHubView View;

        internal static ToolbarControl toolbarControl = null;

        internal const string MODID = "StateFunding_NS";
        internal const string MODNAME = "State Funding";

        #region NO_LOCALIZATION
        public void Start()
        {
            View = new StateFundingHubView();
            //Texture2D Image = new Texture2D(2, 2);
            //Image.LoadImage(File.ReadAllBytes(KSPUtil.ApplicationRootPath+"GameData/StateFunding/assets/cashmoney.png"));
            //Button = ApplicationLauncher.Instance.AddModApplication(onTrue, onFalse, onHover, onHoverOut, onEnable, onDisable, ApplicationLauncher.AppScenes.SPACECENTER, Image);

            toolbarControl = gameObject.AddComponent<ToolbarControl>();
            toolbarControl.AddToAllToolbars(onTrue, onFalse,
                ApplicationLauncher.AppScenes.SPACECENTER,
                MODID,
                "stateFundingButton",
                "StateFunding/assets/cashmoney_38",
                "StateFunding/assets/cashmoney_24",
                MODNAME
            );
        }

        public void unload()
        {
            toolbarControl.OnDestroy();
            Destroy(toolbarControl);
            toolbarControl = null;

        }

        public void onTrue()
        {
            Log.Info("Opened State Funding Hub");
            ViewManager.addView(View);
        }

        public void onFalse()
        {
            Log.Info("Closed State Funding Hub");
            ViewManager.removeView(View);
        }

        #endregion

    }
}

