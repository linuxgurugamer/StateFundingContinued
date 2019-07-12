
namespace StateFunding.Factors
{
    public class ScienceStationsFactor : Factor
    {
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

        public override string GetSummaryText()
        {
            return "Obital Science Stations: " + (int)variables.orbitalScienceStations + "\n" +
                "Planetary Science Stations: " + (int)variables.planetaryScienceStations + "\n";
        }
    }
}
