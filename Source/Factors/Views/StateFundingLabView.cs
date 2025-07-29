using KSP.Localization;
using UnityEngine;
using StateFunding.Factors.Views;
using StateFunding.Factors;

namespace StateFunding
{
    public class StateFundingHubLabView : IFactorView
    {
        public string getSideMenuText()
        {
            return Localizer.Format("#LOC_StateFunding_Science_Stations");
        }

        public void draw(View Vw, ViewWindow Window, Review review)
        {
            Window.title = Localizer.Format("#LOC_StateFunding_Science_Stations");

            InstanceData GameInstance = StateFundingGlobal.fetch.GameInstance;
            if (GameInstance == null)
            {
                Log.Error("StateFundingHubLabView.draw, Inst is null");
                return;
            }

            string Description = Localizer.Format("#LOC_StateFunding_Below_is_a_list_of_existi_DUP4") +
              Localizer.Format("#LOC_StateFunding_Confidence_Landed_station") +
              Localizer.Format("#LOC_StateFunding_To_have_a_qualified_Scien") +
              Localizer.Format("#LOC_StateFunding_power_and_have_at_least_o");

            ViewLabel DescriptionLabel = new ViewLabel(Description);
            DescriptionLabel.setRelativeTo(Window);
            DescriptionLabel.setLeft(140);
            DescriptionLabel.setTop(20);
            DescriptionLabel.setColor(Color.white);
            DescriptionLabel.setHeight(100);
            DescriptionLabel.setWidth(Window.getWidth() - 140);

            Vw.addComponent(DescriptionLabel);

            ViewLabel TotalCoverage = new ViewLabel(Localizer.Format("#LOC_StateFunding_Orbiting_Stations") + " " + (int)review.variables.orbitalScienceStations + ". " +
              Localizer.Format("#LOC_StateFunding_Landed_Stations") + " " + (int)review.variables.planetaryScienceStations + ".");
            TotalCoverage.setRelativeTo(Window);
            TotalCoverage.setLeft(140);
            TotalCoverage.setTop(130);
            TotalCoverage.setColor(Color.white);
            TotalCoverage.setHeight(30);
            TotalCoverage.setWidth(Window.getWidth() - 140);

            Vw.addComponent(TotalCoverage);

            ViewScroll StationsScroll = new ViewScroll();
            StationsScroll.setRelativeTo(Window);
            StationsScroll.setWidth(Window.getWidth() - 140);
            StationsScroll.setHeight(Window.getHeight() - 160);
            StationsScroll.setLeft(140);
            StationsScroll.setTop(150);

            Vw.addComponent(StationsScroll);

            Vessel[] ScienceStations = VesselHelper.GetScienceStations();

            int labelHeight = 20;

            for (int i = 0; i < ScienceStations.Length; i++)
            {
                Vessel ScienceStation = ScienceStations[i];
                string action;
                string target;

                if (ScienceStation.Landed)
                {
                    action = Localizer.Format("#LOC_StateFunding_Landed_At");
                    target = ScienceStation.mainBody.GetName();
                }
                else
                {
                    action = Localizer.Format("#LOC_StateFunding_Orbiting");
                    target = ScienceStation.GetOrbit().referenceBody.GetName();
                }

                string label = ScienceStation.GetName() + Localizer.Format("#LOC_StateFunding_is") + " " + action + " " + target;

                ViewLabel StationLabel = new ViewLabel(label);
                StationLabel.setRelativeTo(StationsScroll);
                StationLabel.setTop(labelHeight + (labelHeight + 5) * i);
                StationLabel.setLeft(0);
                StationLabel.setHeight(labelHeight);
                StationLabel.setWidth(StationsScroll.getWidth() - 20);
                StationLabel.setColor(Color.white);

                StationsScroll.Components.Add(StationLabel);
            }
        }
    }
}

