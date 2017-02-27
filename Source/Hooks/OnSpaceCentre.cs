using System;
using System.Collections;
using UnityEngine;

namespace StateFunding
{

    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class OnSpaceCentre : MonoBehaviour
    {


        public void Awake()
        {
            if (StateFundingGlobal.fetch != null)
            {
                StateFundingGlobal.fetch.unload();
            }

            StateFundingGlobal.fetch = new StateFunding();
        }


        public void Start()
        {
            //ViewManager.removeAll ();
            StateFundingGlobal.fetch.LoadIfNeeded();
        }


        private int curTicks;
        private const int INTERVAL_TICKS = 500;

        public void Update()
        {
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

