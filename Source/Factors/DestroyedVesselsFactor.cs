using KSP.Localization;
using StateFunding.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateFunding.Factors
{
    class DestroyedVesselsFactor : Factor
    {
        public override string FactorName() { return "DestroyedVesselsFactor"; } // NO_LOCALIZATION
        public override int modSC => variables.modSCDestroyedVessels;

        public DestroyedVesselsFactor(FactorVariables factorVariables) : base(factorVariables)
        {
        }

        public override  void Update()
        {
            variables.modSCDestroyedVessels = (int)(1 * (variables.vesselsDestroyed) * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
        }

        public override List<ViewSummaryRow> GetSummaryRow()
        {
            return new List<ViewSummaryRow>()
            {
                new ViewSummaryRow(Localizer.Format("#LOC_StateFunding_Vessels_Destroyed") + variables.vesselsDestroyed, 0, variables.modSCDestroyedVessels)
            };
        }
    }
}
