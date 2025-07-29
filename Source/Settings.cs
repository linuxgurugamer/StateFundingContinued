
using KSP.Localization;
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
        public override string Section { get { return Localizer.Format("#LOC_StateFunding_State_Funding"); } }
        public override string DisplaySection { get { return Localizer.Format("#LOC_StateFunding_State_Funding"); } }
        public override int SectionOrder { get { return 1; } }
        public override bool HasPresets { get { return false; } }


        [GameParameters.CustomParameterUI("Enabled", 
            toolTip = "#LOC_StateFunding_Use_this_to_disable_mod_i")]
        public bool enabled = true;

        [GameParameters.CustomIntParameterUI("Budget Periods Per Year", minValue = 1, maxValue = 66, stepSize = 1,
            toolTip = "#LOC_StateFunding_a_Kerbin_year_is_approxim")]
        public int budgetPeriodsPerYear = 4;

        [GameParameters.CustomParameterUI("Stop warp on new budget period")]
        public bool stopWarpAtBudgetPeriod = true;

        [GameParameters.CustomParameterUI("Stop warp on new year")]
        public bool stopWarpOnNewYear = true;

        // Following strange code is to work around a bugin the CustomFloatParameterUI
        public float convergingRate = 0.5f;
        [GameParameters.CustomFloatParameterUI("Decay Rate (%)", asPercentage = false, displayFormat = "N0", minValue = 0, maxValue = 100, stepCount = 1,
                    toolTip = "#LOC_StateFunding_The_higher_this_is_the_fa")]
        public float ConvergingRate
        {
            get { return convergingRate * 100f; }
            set { convergingRate = value / 100.0f; }
        }

        // Following strange code is to work around a bugin the CustomFloatParameterUI
        [GameParameters.CustomFloatParameterUI("Multiplier", asPercentage = false, displayFormat = "0.0", minValue = 0.1f, maxValue = 2f, stepCount = 21,
                    toolTip = "#LOC_StateFunding_Multiplier_applied_to_the")]
        public float multiplier = 1f;




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
