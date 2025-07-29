using KSP.Localization;
using StateFunding.Factors.Views;
using StateFunding.ViewComponents;
using System;
using System.Collections.Generic;

namespace StateFunding.Factors
{
    public class SatelliteCoverageFactor : Factor
    {
        public override string FactorName() { return "SatelliteCoverageFactor"; } // NO_LOCALIZATION
        public override int modSC => variables.modSCSatellite;
        public override IFactorView View => ((IFactorView)new StateFundingHubCoverageView());

        public SatelliteCoverageFactor(FactorVariables factorVariables) : base(factorVariables)
        {
            variables.satelliteCoverage = 0;
            // Initialize coverage for Celestial Bodies (other than the Sun)

            CelestialBody[] Bodies = FlightGlobals.Bodies.ToArray();
            variables.Coverages = new CoverageReport[Bodies.Length - 1];
            CelestialBody home = Planetarium.fetch.Home;

            if (home == null) home = FlightGlobals.Bodies.Find(body => body.isHomeWorld == true);

            if (home != null) StateFundingGlobal.refRadius = home.Radius / 10;

            int k = 0;

            for (int i = 0; i < Bodies.Length; i++)
            {
                CelestialBody Body = Bodies[i];

                // Don't need to survey the sun
                //if (Body.GetName() != StateFundingGlobal.Sun)
                if (Body != Planetarium.fetch.Sun)
                {
                    CoverageReport Report = new CoverageReport();
                    Report.entity = Body.GetName();

                    // Benchmark: Kerbin
                    // 10 sats till full coverage on Kerbin
                    Report.satCountForFullCoverage = (int)Math.Ceiling(Body.Radius / StateFundingGlobal.refRadius);

                    variables.Coverages[k] = Report;
                    k++;
                }
            }
        }

        public override  void Update()
        {
            Log.Info("Updating Coverage");

            for (int i = 0; i < variables.Coverages.Length; i++)
            {
                variables.Coverages[i].satCount = 0;
            }

            Vessel[] Satellites = VesselHelper.GetSatellites();

            for (int i = 0; i < Satellites.Length; i++)
            {
                Vessel Satellite = Satellites[i];

                CelestialBody Body = Satellite.GetOrbit().referenceBody;
                CoverageReport Report = GetReport(Body.GetName());
                Report.satCount++;
                Report.Update();
            }

            double totalCoverage = 0;
            for (int i = 0; i < variables.Coverages.Length; i++)
            {
                totalCoverage += variables.Coverages[i].coverage;
            }

            variables.satelliteCoverage = totalCoverage / variables.Coverages.Length;
            variables.modSCSatellite = (int)(100 * variables.satelliteCoverage * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
        }

        private CoverageReport GetReport(string bodyName)
        {
            for (var i = 0; i < variables.Coverages.Length; i++)
            {
                CoverageReport Report = variables.Coverages[i];
                if (Report.entity == bodyName)
                {
                    return Report;
                }
            }

            CoverageReport CReport = new CoverageReport();
            CReport.entity = bodyName;

            return CReport;
        }

        public override List<ViewSummaryRow> GetSummaryRow()
        {
            return new List<ViewSummaryRow>()
            {
                new ViewSummaryRow(Localizer.Format("#LOC_StateFunding_Satellite_Coverage") + " "+ Math.Round(variables.satelliteCoverage * 100) + "%", 0, variables.modSCSatellite)
            };
        }
    }
}
