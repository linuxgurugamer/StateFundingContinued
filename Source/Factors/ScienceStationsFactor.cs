
using StateFunding.ViewComponents;
using System;
using System.Collections.Generic;

namespace StateFunding.Factors
{
    public class ScienceStationsFactor : Factor
    {
        public override string FactorName() { return "ScienceStationsFactor"; }
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
                new ViewSummaryRow("Obital Science Stations: " + (int)variables.orbitalScienceStations, 0, (int)(2 * variables.orbitalScienceStations * StateFundingGlobal.fetch.GameInstance.Gov.scModifier)),
                new ViewSummaryRow("Planetary Science Stations: " + (int)variables.planetaryScienceStations, 0, (int)(5 * variables.planetaryScienceStations * StateFundingGlobal.fetch.GameInstance.Gov.scModifier))
            };
        }
    }
}
