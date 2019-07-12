
using StateFunding.Factors.Views;
using System.Collections.Generic;

namespace StateFunding.Factors
{
    public class RoversFactor : Factor
    {
        public override int modPO => variables.modPORovers;
        public override IFactorView View => ((IFactorView)new StateFundingHubRoversView());
        

        public RoversFactor(FactorVariables factorVariables) : base(factorVariables)
        {
            variables.rovers = 0;
        }

        public override  void Update()
        {
            Log.Info("Updating Rovers");
            variables.rovers = VesselHelper.GetRovers().Length;
            variables.modPORovers = (int)(5 * variables.rovers * StateFundingGlobal.fetch.GameInstance.Gov.poModifier);
        }

        public override string GetSummaryText()
        {
            return "Rovers: " + variables.rovers + "\n";
        }
    }
}
