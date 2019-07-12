
using StateFunding.Factors.Views;
using System.Collections.Generic;

namespace StateFunding.Factors
{
    public class MiningRigsFactor : Factor
    {
        public override int modSC => variables.modSCMiningRig;
        public override IFactorView View => ((IFactorView)new StateFundingHubMiningView());

        public static string miningRigs = "miningRigs";

        public MiningRigsFactor(FactorVariables factorVariables) : base(factorVariables)
        {
        }

        public override  void Update()
        {
            Log.Info("Updating Mining Rigs");
            variables.miningRigs = VesselHelper.GetMiningRigs().Length;
            variables.modSCMiningRig = (int)(5 * variables.miningRigs * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
        }

        public override string GetSummaryText()
        {
            return "Active Mining Rigs: " + (int)variables.miningRigs + "\n";
        }
    }
}
