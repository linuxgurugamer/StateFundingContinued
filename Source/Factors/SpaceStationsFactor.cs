using StateFunding.Factors.Views;
using System.Collections.Generic;
using System.Linq;

namespace StateFunding.Factors
{
    public class SpaceStationsFactor : Factor
    {
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
                SpcStationReport.drill = VesselHelper.VesselHasModuleAlias(SpcStation, "Drill");
                SpcStationReport.scienceLab = VesselHelper.VesselHasModuleAlias(SpcStation, "ScienceLab");
                SpcStationReport.fuel = VesselHelper.GetResourceCount(SpcStation, "LiquidFuel");
                SpcStationReport.ore = VesselHelper.GetResourceCount(SpcStation, "Ore");
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

        public override string GetSummaryText()
        {
            string returnText = "";
            if ((variables.SpaceStations != null) && (variables.SpaceStations.Length > 0))
            {
                returnText += "\n\n== Space Stations ==\n\n";
                for (int i = 0; i < variables.SpaceStations.Length; i++)
                {
                    SpaceStationReport StationReport = variables.SpaceStations[i];
                    returnText += "[" + StationReport.name + " Orbiting " + StationReport.entity + "]\n";
                    returnText += "Fuel: " + StationReport.fuel + "\n";
                    returnText += "Ore: " + StationReport.ore + "\n";
                    returnText += "Crew: " + StationReport.crew + "\n";
                    returnText += "Crew Capacity: " + StationReport.crewCapacity + "\n";
                    returnText += "Docked Vessels: " + StationReport.dockedVessels + "\n";
                    returnText += "Docking Ports: " + StationReport.dockingPorts + "\n";
                    returnText += "Has Drill: " + StationReport.drill + "\n";
                    returnText += "Science Lab: " + StationReport.scienceLab + "\n";
                    returnText += "On Asteroid: " + StationReport.onAsteroid + "\n";
                    returnText += "PO: " + StationReport.po + "\n";
                    returnText += "SC: " + StationReport.sc + "\n\n";
                }
            }
            return returnText;
        }
    }
}
