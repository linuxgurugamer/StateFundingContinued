using KSP.Localization;
using UnityEngine;
using StateFunding.Factors.Views;
using StateFunding.Factors;

namespace StateFunding
{
    public class StateFundingHubRoversView : IFactorView
    {
        public string getSideMenuText()
        {
            return Localizer.Format("#LOC_StateFunding_Rovers_DUP1");
        }

        public void draw(View Vw, ViewWindow Window, Review review)
        {
            Window.title = Localizer.Format("#LOC_StateFunding_Rovers_DUP1");

            InstanceData GameInstance = StateFundingGlobal.fetch.GameInstance;
            if (GameInstance == null)
            {
                Log.Error("StateFundingHubRoversView.draw, Inst is null");
                return;
            }

            string Description = Localizer.Format("#LOC_StateFunding_Below_is_a_list_of_existi_DUP2") +
              Localizer.Format("#LOC_StateFunding_Vessels_that_are_rovers_s") +
              Localizer.Format("#LOC_StateFunding_If_any_wheels_on_the_rove") +
              Localizer.Format("#LOC_StateFunding_than_the_home_planet_Kerb");

            ViewLabel DescriptionLabel = new ViewLabel(Description);
            DescriptionLabel.setRelativeTo(Window);
            DescriptionLabel.setLeft(140);
            DescriptionLabel.setTop(20);
            DescriptionLabel.setColor(Color.white);
            DescriptionLabel.setHeight(100);
            DescriptionLabel.setWidth(Window.getWidth() - 140);

            Vw.addComponent(DescriptionLabel);

            ViewLabel TotalRovers = new ViewLabel(Localizer.Format("#LOC_StateFunding_Total_Rovers") + " " + review.variables.rovers);
            TotalRovers.setRelativeTo(Window);
            TotalRovers.setLeft(140);
            TotalRovers.setTop(130);
            TotalRovers.setColor(Color.white);
            TotalRovers.setHeight(30);
            TotalRovers.setWidth(Window.getWidth() - 140);

            Vw.addComponent(TotalRovers);

            ViewScroll RoversScroll = new ViewScroll();
            RoversScroll.setRelativeTo(Window);
            RoversScroll.setWidth(Window.getWidth() - 140);
            RoversScroll.setHeight(Window.getHeight() - 160);
            RoversScroll.setLeft(140);
            RoversScroll.setTop(150);

            Vw.addComponent(RoversScroll);

            Vessel[] Rovers = VesselHelper.GetRovers();

            int labelHeight = 20;

            for (int i = 0; i < Rovers.Length; i++)
            {
                Vessel Rover = Rovers[i];
                //string target;

                string label = Rover.GetName() + Localizer.Format("#LOC_StateFunding_is_Landed_at") + " " + Rover.mainBody.GetName();

                ViewLabel RoverLabel = new ViewLabel(label);
                RoverLabel.setRelativeTo(RoversScroll);
                RoverLabel.setTop(labelHeight + (labelHeight + 5) * i);
                RoverLabel.setLeft(0);
                RoverLabel.setHeight(labelHeight);
                RoverLabel.setWidth(RoversScroll.getWidth() - 20);
                RoverLabel.setColor(Color.white);

                RoversScroll.Components.Add(RoverLabel);
            }

        }
    }
}

