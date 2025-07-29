using KSP.Localization;
using StateFunding.Factors.Views;
using StateFunding.ViewComponents;
using System.Collections.Generic;
using System.Linq;

namespace StateFunding.Factors
{
    public class SpaceStationsFactor : Factor
    {
        public override string FactorName() { return "SpaceStationsFactor"; } // NO_LOCALIZATION
        public override int modPO => variables.SpaceStations.Sum(x => x.po);
        public override int modSC => variables.SpaceStations.Sum(x => x.sc);
        public override IFactorView View => ((IFactorView)new StateFundingHubStationsView());

        public SpaceStationsFactor(FactorVariables factorVariables) : base(factorVariables)
        {
        }

        public override  void Update()
        {
            Log.Info("Updating Space Stations");

            InstanceData GameInstance = StateFundingGlobal.fetch.GameInstance;
            if (GameInstance == null)
            {
                Log.Error("Review.UpdateSpaceStations, GameInstance is null");
                return;
            }

            Vessel[] vessels = VesselHelper.GetSpaceStations();
            variables.SpaceStations = new SpaceStationReport[vessels.Length];

            for (int i = 0; i < vessels.Length; i++)
            {
                Vessel SpcStation = vessels[i];

                SpaceStationReport SpcStationReport = new SpaceStationReport();
                SpcStationReport.name = SpcStation.vesselName;
                SpcStationReport.crew = VesselHelper.GetCrew(SpcStation).Length;
                SpcStationReport.crewCapacity = VesselHelper.GetCrewCapactiy(SpcStation);
                SpcStationReport.dockedVessels = VesselHelper.GetDockedVesselsCount(SpcStation);
                SpcStationReport.dockingPorts = VesselHelper.GetDockingPorts(SpcStation).Length;
                SpcStationReport.drill = VesselHelper.VesselHasModuleAlias(SpcStation, "Drill"); // NO_LOCALIZATION
                SpcStationReport.scienceLab = VesselHelper.VesselHasModuleAlias(SpcStation, "ScienceLab"); // NO_LOCALIZATION
                SpcStationReport.fuel = VesselHelper.GetResourceCount(SpcStation, "LiquidFuel"); // NO_LOCALIZATION
                SpcStationReport.ore = VesselHelper.GetResourceCount(SpcStation, "Ore"); // NO_LOCALIZATION
                SpcStationReport.onAsteroid = VesselHelper.OnAsteroid(SpcStation);

                if (SpcStation.Landed)
                {
                    SpcStationReport.entity = SpcStation.landedAt;
                }
                else
                {
                    SpcStationReport.entity = SpcStation.GetOrbit().referenceBody.GetName();
                }

                SpcStationReport.po = 0;
                SpcStationReport.sc = 0;

                SpcStationReport.po += (int)(5 * SpcStationReport.crew * GameInstance.Gov.poModifier);
                SpcStationReport.po += (int)(5 * SpcStationReport.dockedVessels * GameInstance.Gov.poModifier);

                if (SpcStationReport.onAsteroid)
                {
                    SpcStationReport.po += (int)(30 * GameInstance.Gov.poModifier);

                    if (SpcStationReport.drill)
                    {
                        SpcStationReport.po += (int)(10 * GameInstance.Gov.poModifier);
                        SpcStationReport.sc += (int)(10 * GameInstance.Gov.scModifier);
                    }
                }

                SpcStationReport.sc += (int)(2 * SpcStationReport.crewCapacity * GameInstance.Gov.scModifier);
                SpcStationReport.sc += (int)(SpcStationReport.fuel / 200f * GameInstance.Gov.scModifier);
                SpcStationReport.sc += (int)(SpcStationReport.ore / 200f * GameInstance.Gov.scModifier);
                SpcStationReport.sc += (int)(2 * SpcStationReport.dockingPorts * GameInstance.Gov.scModifier);
                SpcStationReport.sc += (int)(2 * SpcStationReport.crewCapacity * GameInstance.Gov.scModifier);

                if (SpcStationReport.scienceLab)
                {
                    SpcStationReport.po += (int)(10 * GameInstance.Gov.poModifier);
                    SpcStationReport.sc += (int)(10 * GameInstance.Gov.scModifier);
                }

                variables.SpaceStations[i] = SpcStationReport;
            }
        }

        public override List<ViewSummaryRow> GetSummaryRow()
        {
            return new List<ViewSummaryRow>()
            {
                new ViewSummaryRow(Localizer.Format("#LOC_StateFunding_Space_Stations") + " "+ (int)variables.SpaceStations.Length, variables.SpaceStations.Sum(x => x.po), variables.SpaceStations.Sum(x => x.sc))
            };
        }
    }
}
