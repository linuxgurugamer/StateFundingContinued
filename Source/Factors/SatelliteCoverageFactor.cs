using StateFunding.Factors.Views;
using System;
using System.Collections.Generic;

namespace StateFunding.Factors
{
    public class SatelliteCoverageFactor : Factor
    {
        [Persistent]
        public CoverageReport[] Coverages;

        public override int modSC => _modSC;
        public override IFactorView View => ((IFactorView)new StateFundingHubCoverageView());
        private int _modSC = 0;

        public static string satelliteCoverage = "satelliteCoverage";

        public SatelliteCoverageFactor(Dictionary<string, double> factorVariables) : base(factorVariables)
        {
            factorVariables[satelliteCoverage] = 0;
            // Initialize coverage for Celestial Bodies (other than the Sun)

            CelestialBody[] Bodies = FlightGlobals.Bodies.ToArray();
            Coverages = new CoverageReport[Bodies.Length - 1];
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

                    Coverages[k] = Report;
                    k++;
                }
            }
        }

        public override  void Update(Dictionary<string, double> factorVariables)
        {
            Log.Info("Updating Coverage");

            for (int i = 0; i < Coverages.Length; i++)
            {
                Coverages[i].satCount = 0;
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

            float totalCoverage = 0;
            for (int i = 0; i < Coverages.Length; i++)
            {
                totalCoverage += Coverages[i].coverage;
            }

            factorVariables[satelliteCoverage] = (float)totalCoverage / (float)Coverages.Length;
            _modSC = (int)(2 * factorVariables[satelliteCoverage] * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
        }

        private CoverageReport GetReport(string bodyName)
        {
            for (var i = 0; i < Coverages.Length; i++)
            {
                CoverageReport Report = Coverages[i];
                if (Report.entity == bodyName)
                {
                    return Report;
                }
            }

            CoverageReport CReport = new CoverageReport();
            CReport.entity = bodyName;

            return CReport;
        }

        public override string GetSummaryText(Dictionary<string, double> factorVariables)
        {
            return "Satellite Coverage: " + Math.Round(factorVariables[satelliteCoverage] * 100) + "%\n";
        }
    }
}
