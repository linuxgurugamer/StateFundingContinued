
using StateFunding.Factors.Views;
using System.Collections.Generic;

namespace StateFunding.Factors
{
    public class RoversFactor : Factor
    {
        public override int modPO => _modPO;
        public override IFactorView View => ((IFactorView)new StateFundingHubRoversView());
        private int _modPO = 0;

        public static string rovers = "rovers";

        public RoversFactor(Dictionary<string, double> factorVariables) : base(factorVariables)
        {
            factorVariables[rovers] = 0;
        }

        public override  void Update(Dictionary<string, double> factorVariables)
        {
            Log.Info("Updating Rovers");
            factorVariables[rovers] = VesselHelper.GetRovers().Length;
            _modPO = (int)(5 * factorVariables[rovers] * StateFundingGlobal.fetch.GameInstance.Gov.poModifier);
        }

        public override string GetSummaryText(Dictionary<string, double> factorVariables)
        {
            return "Rovers: " + rovers + "\n";
        }
    }
}
