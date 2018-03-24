
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;



namespace StateFunding
{
    // http://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/#comment-2754813
    // search for "Mod integration into Stock Settings

    public class StateFundingSettings : GameParameters.CustomParameterNode
    {
        public override string Title { get { return ""; } } // Column header
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "State Funding"; } }
        public override string DisplaySection { get { return "State Funding"; } }
        public override int SectionOrder { get { return 1; } }
        public override bool HasPresets { get { return false; } }


        [GameParameters.CustomParameterUI("Enabled", 
            toolTip = "Use this to disable mod in existing game")]
        public bool enabled = true;

        [GameParameters.CustomParameterUI("Use Blizzy Toolbar, if avaliable")]
        public bool useBlizzy = false;

        [GameParameters.CustomIntParameterUI("Budget Periods Per Year", minValue = 1, maxValue = 66, stepSize = 1,
            toolTip = "(a Kerbin year is approximately 66 months")]
        public int budgetPeriodsPerYear = 4;

        [GameParameters.CustomParameterUI("Stop warp on new budget period")]
        public bool stopWarpAtBudgetPeriod = true;

        [GameParameters.CustomParameterUI("Stop warp on new year")]
        public bool stopWarpOnNewYear = true;


#if false
        public override void SetDifficultyPreset(GameParameters.Preset preset)
        {
            switch (preset)
            {
                case GameParameters.Preset.Easy:
                    toolbarEnabled = true;
                    toolbarPopupsEnabled = true;
                    editorMenuPopupEnabled = true;
                    hoverTimeout = 0.5f;
                    break;

                case GameParameters.Preset.Normal:
                    toolbarEnabled = true;
                    toolbarPopupsEnabled = true;
                    editorMenuPopupEnabled = true;
                    hoverTimeout = 0.5f;
                    break;

                case GameParameters.Preset.Moderate:
                    toolbarEnabled = true;
                    toolbarPopupsEnabled = true;
                    editorMenuPopupEnabled = true;
                    hoverTimeout = 0.5f;
                    break;

                case GameParameters.Preset.Hard:
                    toolbarEnabled = true;
                    toolbarPopupsEnabled = true;
                    editorMenuPopupEnabled = true;
                    hoverTimeout = 0.5f;
                    break;
            }
        }
#endif

        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            //if (member.Name == "enabled")
            //    return true;

            return true; //otherwise return true
        }

        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {

            return true;
            //            return true; //otherwise return true
        }

        public override IList ValidValues(MemberInfo member)
        {
            return null;
        }

    }
}
