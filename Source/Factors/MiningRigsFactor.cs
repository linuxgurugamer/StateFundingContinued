
using StateFunding.Factors.Views;
using System.Collections.Generic;

namespace StateFunding.Factors
{
    public class MiningRigsFactor : Factor
    {
        public override int modSC => _modSC;
        public override IFactorView View => ((IFactorView)new StateFundingHubMiningView());
        private int _modSC = 0;

        public static string miningRigs = "miningRigs";

        public MiningRigsFactor(Dictionary<string, double> factorVariables) : base(factorVariables)
        {
            factorVariables[miningRigs] = 0;
        }

        public override  void Update(Dictionary<string, double> factorVariables)
        {
            Log.Info("Updating Mining Rigs");
            factorVariables[miningRigs] = VesselHelper.GetMiningRigs().Length;
            _modSC = (int)(5 * factorVariables[miningRigs] * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
        }

        public override string GetSummaryText(Dictionary<string, double> factorVariables)
        {
            return "Active Mining Rigs: " + (int)factorVariables[miningRigs] + "\n";
        }
    }
}
