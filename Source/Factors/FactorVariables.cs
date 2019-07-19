
namespace StateFunding.Factors
{
    public class FactorVariables
    {
        //ground bases
        [Persistent]
        public BaseReport[] Bases = new BaseReport[0];

        //contracts
        [Persistent]
        public int contractsCompleted = 0;
        [Persistent]
        public int contractsFailed = 0;
        [Persistent]
        public int modSCContracts = 0;

        //contracts
        [Persistent]
        public int activeKerbals = 0;
        [Persistent]
        public int strandedKerbals = 0;
        [Persistent]
        public int kerbalDeaths = 0;
        [Persistent]
        public int modPOKerbals = 0;

        //science stations and bases
        [Persistent]
        public int orbitalScienceStations = 0;
        [Persistent]
        public int planetaryScienceStations = 0;
        [Persistent]
        public int modSCScienceStations = 0;

        //space stations
        [Persistent]
        public SpaceStationReport[] SpaceStations = new SpaceStationReport[0];

        //rovers
        [Persistent]
        public int rovers = 0;
        [Persistent]
        public int modPORovers = 0;

        //mining rig
        [Persistent]
        public int miningRigs = 0;
        [Persistent]
        public int modSCMiningRig = 0;

        //satellite coverage
        [Persistent]
        public CoverageReport[] Coverages = new CoverageReport[0];
        [Persistent]
        public double satelliteCoverage = 0;
        [Persistent]
        public int modSCSatellite = 0;

        //destroyed vessels
        [Persistent]
        public int vesselsDestroyed = 0;
        [Persistent]
        public int modSCDestroyedVessels = 0;

    }
}
