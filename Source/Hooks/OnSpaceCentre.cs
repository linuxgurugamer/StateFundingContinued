using System;
using System.Collections;
using UnityEngine;

namespace StateFunding
{

    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class OnSpaceCentre : MonoBehaviour
    {

        internal static OnSpaceCentre Instance;

        public void Awake()
        {
            if (!HighLogic.CurrentGame.Parameters.CustomParams<StateFundingSettings>().enabled || HighLogic.CurrentGame.Mode != Game.Modes.CAREER)
                return;
            if (StateFundingGlobal.fetch != null)
            {
                StateFundingGlobal.fetch.unload();
            }

            StateFundingGlobal.fetch = new StateFunding();
            TimeHelper.SetKSPStockCalendar();
        }


        public void Start()
        {
            Instance = this;
            
            if (!HighLogic.CurrentGame.Parameters.CustomParams<StateFundingSettings>().enabled || HighLogic.CurrentGame.Mode != Game.Modes.CAREER)
                return;

            //ViewManager.removeAll ();
            StateFundingGlobal.fetch.LoadIfNeeded();
        }


        private int curTicks;
        private const int INTERVAL_TICKS = 500;

        public void Update()
        {
            if (!HighLogic.CurrentGame.Parameters.CustomParams<StateFundingSettings>().enabled || HighLogic.CurrentGame.Mode != Game.Modes.CAREER)
                return;

            // Update once every interval updates
            curTicks++;
            if (curTicks > INTERVAL_TICKS)
            {
                curTicks = 0;
                if (StateFundingGlobal.fetch != null)
                {
                    StateFundingGlobal.fetch.tick();
                }
            }
        }



    }
}

