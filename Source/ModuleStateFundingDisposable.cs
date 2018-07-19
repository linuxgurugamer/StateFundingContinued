using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.UI.Screens;

namespace StateFunding
{
    public class ModuleStateFundingDisposable : PartModule
    {
        [KSPField(guiActive = false, guiName = "Disposable", isPersistant = true)]
        public bool disposable = false;

        [KSPField(guiActive = false, guiName = "AlwaysDisposable", isPersistant = true)]
        public bool alwaysDisposable = false;

        [KSPEvent(guiActiveEditor = true, guiActive = false, guiActiveUnfocused = true, guiName = "Not Disposable")]
        public void ToggleExpendable()
        {
            disposable = !disposable;
            UpdateExpendableGuiName();
        }
        void UpdateExpendableGuiName()
        {
            if (disposable)
            {
                Events["ToggleExpendable"].guiName = "Currently Disposable";
            }
            else
            {
                Events["ToggleExpendable"].guiName = "Not Disposable";
            }

        }
        public override string GetInfo()
        {
            return "ModuleStateFundingDisposable";
        }
        void Start()
        {
            if (alwaysDisposable)
            {
                Events["ToggleExpendable"].active = false;
                disposable = true;
                Fields["disposable"].guiActive = false;
            } else
            if (HighLogic.LoadedSceneIsFlight)
            {
                Fields["disposable"].guiActive = true;
            }
            UpdateExpendableGuiName();
        }
    }
}
