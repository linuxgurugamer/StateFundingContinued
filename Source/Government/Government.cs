using KSP.Localization;
using System;

namespace StateFunding
{
    public class Government
    {

        public float budget;
        public int budgetPeriodsPerYear;
        public string description;
        public float gdp;
        public string longName;
        public String name;
        public float poModifier;
        public float poPenaltyModifier;
        public float scModifier;
        public float scPenaltyModifier;
        public int startingPO;
        public int startingSC;

        public Government() { }

        private string modifierLexicon(float val)
        {
            if (val <= 0.25)
            {
                return Localizer.Format("#LOC_StateFunding_Very_Low");
            }
            else if (val <= 0.5)
            {
                return Localizer.Format("#LOC_StateFunding_Low");
            }
            else if (val <= 1)
            {
                return Localizer.Format("#LOC_StateFunding_Normal");
            }
            else if (val <= 2)
            {
                return Localizer.Format("#LOC_StateFunding_High");
            }

            return Localizer.Format("#LOC_StateFunding_Very_High");
        }

        public string GetGameplayDescription()
        {
            return Localizer.Format("#LOC_StateFunding_GDP") + " " + gdp.ToString("#,##0") + "\n" +
              Localizer.Format("#LOC_StateFunding_Yearly_Budget") + " " + (gdp * budget).ToString("#,##0") + "\n" +
              Localizer.Format("#LOC_StateFunding_Budget_Periods_Per_year") + " " + budgetPeriodsPerYear.ToString() + "\n" +
              Localizer.Format("#LOC_StateFunding_Starting_PO") + " " + startingPO + "\n" +
              Localizer.Format("#LOC_StateFunding_Starting_SC") + " " + startingSC + "\n" +
              Localizer.Format("#LOC_StateFunding_State_Reward") + " " + modifierLexicon(scModifier) + "\n" +
              Localizer.Format("#LOC_StateFunding_State_Penalty") + " " + modifierLexicon(scPenaltyModifier) + "\n" +
              Localizer.Format("#LOC_StateFunding_Public_Reward") + " " + modifierLexicon(poModifier) + " \n" +
              Localizer.Format("#LOC_StateFunding_Public_Penalty") + " " + modifierLexicon(poPenaltyModifier) + " \n";
        }

    }
}

