
using KSP.Localization;
using StateFunding.ViewComponents;
using System;
using System.Collections.Generic;

namespace StateFunding.Factors
{
    public class ScienceStationsFactor : Factor
    {
        public override string FactorName() { return "ScienceStationsFactor"; } // NO_LOCALIZATION
        public override int modSC => variables.modSCScienceStations;

        public ScienceStationsFactor(FactorVariables factorVariables) : base(factorVariables)
        {
        }

        public override  void Update()
        {
            Log.Info("Updating Science Stations");
            variables.orbitalScienceStations = VesselHelper.GetOrbitingScienceStations().Length;
            variables.planetaryScienceStations = VesselHelper.GetLandedScienceStations().Length;
            variables.modSCScienceStations = (int)(2 * variables.orbitalScienceStations * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
            variables.modSCScienceStations += (int)(5 * variables.planetaryScienceStations * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
        }

        public override List<ViewSummaryRow> GetSummaryRow()
        {
            return new List<ViewSummaryRow>()
            {
                new ViewSummaryRow(Localizer.Format("#LOC_StateFunding_Obital_Science_Stations") + " "+ (int)variables.orbitalScienceStations, 0, (int)(2 * variables.orbitalScienceStations * StateFundingGlobal.fetch.GameInstance.Gov.scModifier)),
                new ViewSummaryRow(Localizer.Format("#LOC_StateFunding_Planetary_Science_Station") + " "+ (int)variables.planetaryScienceStations, 0, (int)(5 * variables.planetaryScienceStations * StateFundingGlobal.fetch.GameInstance.Gov.scModifier))
            };
        }
    }
}
