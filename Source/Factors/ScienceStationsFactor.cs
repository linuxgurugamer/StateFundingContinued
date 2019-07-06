
using System.Collections.Generic;

namespace StateFunding.Factors
{
    public class ScienceStationsFactor : Factor
    {
        public override int modSC => _modSC;
        private int _modSC = 0;

        public static string orbitalScienceStations = "orbitalScienceStations";
        public static string planetaryScienceStations = "planetaryScienceStations";

        public ScienceStationsFactor(Dictionary<string, double> factorVariables) : base(factorVariables)
        {
            factorVariables[orbitalScienceStations] = 0;
            factorVariables[planetaryScienceStations] = 0;
        }

        public override  void Update(Dictionary<string, double> factorVariables)
        {
            Log.Info("Updating Science Stations");
            factorVariables[orbitalScienceStations] = VesselHelper.GetOrbitingScienceStations().Length;
            factorVariables[planetaryScienceStations] = VesselHelper.GetLandedScienceStations().Length;
            _modSC = (int)(2 * factorVariables[orbitalScienceStations] * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
            _modSC += (int)(5 * factorVariables[planetaryScienceStations] * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
        }

        public override string GetSummaryText(Dictionary<string, double> factorVariables)
        {
            return "Obital Science Stations: " + (int)factorVariables[orbitalScienceStations] + "\n" +
                   "Planetary Science Stations: " + (int)factorVariables[planetaryScienceStations] + "\n";
        }
    }
}
