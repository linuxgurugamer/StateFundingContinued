
using KSP.Localization;
using StateFunding.Factors.Views;
using StateFunding.ViewComponents;
using System.Collections.Generic;

namespace StateFunding.Factors
{
    public class MiningRigsFactor : Factor
    {
        public override string FactorName() { return "MiningRigsFactor"; } // NO_LOCALIZATION
        public override int modSC => variables.modSCMiningRig;
        public override IFactorView View => ((IFactorView)new StateFundingHubMiningView());

        public static string miningRigs = "miningRigs"; // NO_LOCALIZATION

        public MiningRigsFactor(FactorVariables factorVariables) : base(factorVariables)
        {
        }

        public override  void Update()
        {
            Log.Info("Updating Mining Rigs");
            variables.miningRigs = VesselHelper.GetMiningRigs().Length;
            variables.modSCMiningRig = (int)(5 * variables.miningRigs * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
        }

        public override List<ViewSummaryRow> GetSummaryRow()
        {
            return new List<ViewSummaryRow>()
            {
                new ViewSummaryRow(Localizer.Format("#LOC_StateFunding_Active_Mining_Rigs") + " "+ (int)variables.miningRigs, 0, variables.modSCMiningRig)
            };
        }
    }
}
