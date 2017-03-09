using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateFunding
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class ViewManager : MonoBehaviour
    {

        public static List<View> Views = new List<View>();

        public void Start()
        {
            //DontDestroyOnLoad(this);
        }

        public static void addView(View V)
        {
            ViewManager.Views.Add(V);
        }

        // TODO: Fix this. It sucks
        public static void removeView(View V)
        {
            //ViewManager.Views.Remove (V);
            ViewManager.removeAll();
        }

        public static void removeAll()
        {
            ViewManager.Views.Clear();
        }

        public void OnGUI()
        {
            if (!HighLogic.CurrentGame.Parameters.CustomParams<StateFundingSettings>().enabled || HighLogic.CurrentGame.Mode != Game.Modes.CAREER)
                return;

            for (var i = 0; i < ViewManager.Views.ToArray().Length; i++)
            {
                View V = ViewManager.Views.ToArray()[i];
                if (V.isPainting())
                {
                    V.paint();
                }
            }
        }

    }
}

