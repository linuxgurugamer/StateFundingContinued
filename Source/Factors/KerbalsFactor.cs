
using StateFunding.Factors.Views;
using System;
using System.Collections.Generic;

namespace StateFunding.Factors
{
    public class KerbalsFactor : Factor
    {
        public override int modPO => _modPO;
        public override IFactorView View => new StateFundingHubKerbalsView();

        private int _modPO;

        public static string activeKerbals = "activeKerbals";
        public static string strandedKerbals = "strandedKerbals";
        public static string kerbalDeaths = "kerbalDeaths";

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

        public override string GetSummaryText()
        {
            return 
                "Active Kerbals: " + (int)variables.activeKerbals + "\n" +
                "Kerbal \"Accidents\": " + (int)variables.kerbalDeaths + "\n" +
                "Stranded Kerbals: " + (int)variables.strandedKerbals + "\n";
        }
    }
}
