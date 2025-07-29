
using KSP.Localization;
using StateFunding.Factors.Views;
using StateFunding.ViewComponents;
using System;
using System.Collections.Generic;

namespace StateFunding.Factors
{
    public class KerbalsFactor : Factor
    {
        public override int modPO => _modPO;
        public override IFactorView View => new StateFundingHubKerbalsView();

        private int _modPO;

        public static string activeKerbals = "activeKerbals"; // NO_LOCALIZATION
        public static string strandedKerbals = "strandedKerbals"; // NO_LOCALIZATION
        public static string kerbalDeaths = "kerbalDeaths"; // NO_LOCALIZATION

        public KerbalsFactor(FactorVariables factorVariables) : base (factorVariables)
        {
        }

        public override void Update()
        {
            Log.Info("Updating Active Kerbals");
            variables.activeKerbals = KerbalHelper.GetActiveKerbals().Length;
            variables.strandedKerbals = KerbalHelper.GetStrandedKerbals().Length;
            _modPO = (int)(5 * variables.activeKerbals * StateFundingGlobal.fetch.GameInstance.Gov.poModifier);
            _modPO -= (int)(5 * variables.strandedKerbals * StateFundingGlobal.fetch.GameInstance.Gov.poModifier);
            _modPO -= (int)(5 * variables.kerbalDeaths * StateFundingGlobal.fetch.GameInstance.Gov.poModifier);
        }

        public override List<ViewSummaryRow> GetSummaryRow()
        {
            return new List<ViewSummaryRow>()
            {
                new ViewSummaryRow(Localizer.Format("#LOC_StateFunding_Active_Kerbals")+ " " + (int)variables.activeKerbals, (int)(5 * variables.activeKerbals * StateFundingGlobal.fetch.GameInstance.Gov.poModifier), 0),
                new ViewSummaryRow(Localizer.Format("#LOC_StateFunding_Kerbal_Accidents") + " "+ (int)variables.kerbalDeaths, 0, (int)(-5 * variables.strandedKerbals * StateFundingGlobal.fetch.GameInstance.Gov.poModifier)),
                new ViewSummaryRow(Localizer.Format("#LOC_StateFunding_Stranded_Kerbals") + " "+ (int)variables.strandedKerbals, 0, (int)(-5 * variables.kerbalDeaths * StateFundingGlobal.fetch.GameInstance.Gov.poModifier))
            };
        }
    }
}
