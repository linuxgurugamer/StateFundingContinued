
using StateFunding.Factors.Views;
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

        public KerbalsFactor(Dictionary<string, double> factorVariables) : base (factorVariables)
        {
            factorVariables[activeKerbals] = 0;
            factorVariables[strandedKerbals] = 0;
            factorVariables[kerbalDeaths] = 0;
        }

        public override void Update(Dictionary<string, double> factorVariables)
        {
            Log.Info("Updating Active Kerbals");
            factorVariables[activeKerbals] = KerbalHelper.GetActiveKerbals().Length;
            factorVariables[strandedKerbals] = KerbalHelper.GetStrandedKerbals().Length;
            _modPO = (int)(5 * factorVariables[activeKerbals] * StateFundingGlobal.fetch.GameInstance.Gov.poModifier);
            _modPO -= (int)(5 * factorVariables[strandedKerbals] * StateFundingGlobal.fetch.GameInstance.Gov.poModifier);
            _modPO -= (int)(5 * factorVariables[kerbalDeaths] * StateFundingGlobal.fetch.GameInstance.Gov.poModifier);
        }

        public override string GetSummaryText(Dictionary<string, double> factorVariables)
        {
            return "Active Kerbals: " + (int)factorVariables[activeKerbals] + "\n" +
                   "Kerbal \"Accidents\": " + (int)factorVariables[kerbalDeaths] + "\n" +
                   "Stranded Kerbals: " + (int)factorVariables[strandedKerbals] + "\n";
        }
    }
}
