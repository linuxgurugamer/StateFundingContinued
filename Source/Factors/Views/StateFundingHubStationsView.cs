using KSP.Localization;
using StateFunding.Factors;
using StateFunding.Factors.Views;
using UnityEngine;

namespace StateFunding
{
    public class StateFundingHubStationsView : IFactorView
    {
        public string getSideMenuText()
        {
            return Localizer.Format("#LOC_StateFunding_Space_Stations_DUP1");
        }

        public void draw(View Vw, ViewWindow Window, Review review)
        {
            Window.title = Localizer.Format("#LOC_StateFunding_Space_Stations_DUP1");

            InstanceData GameInstance = StateFundingGlobal.fetch.GameInstance;
            if (GameInstance == null)
            {
                Log.Error("StateFundingHubStationsView.draw, Inst is null");
                return;
            }

            string Description = Localizer.Format("#LOC_StateFunding_Below_is_a_list_of_existi_DUP3") +
              Localizer.Format("#LOC_StateFunding_such_be_in_orbit_and_must") +
              Localizer.Format("#LOC_StateFunding_Space_Stations_are_scored") +
              Localizer.Format("#LOC_StateFunding_Port_Count_SC_Docked_Vess") +
              Localizer.Format("#LOC_StateFunding_get_a_bonus_PO_If_you_are");

            ViewLabel DescriptionLabel = new ViewLabel(Description);
            DescriptionLabel.setRelativeTo(Window);
            DescriptionLabel.setLeft(140);
            DescriptionLabel.setTop(20);
            DescriptionLabel.setColor(Color.white);
            DescriptionLabel.setHeight(100);
            DescriptionLabel.setWidth(Window.getWidth() - 140);

            Vw.addComponent(DescriptionLabel);

            ViewLabel TotalStations = new ViewLabel(Localizer.Format("#LOC_StateFunding_Total_Stations") + " " + review.variables.SpaceStations.Length);
            TotalStations.setRelativeTo(Window);
            TotalStations.setLeft(140);
            TotalStations.setTop(130);
            TotalStations.setColor(Color.white);
            TotalStations.setHeight(30);
            TotalStations.setWidth(Window.getWidth() - 140);

            Vw.addComponent(TotalStations);

            ViewScroll StationsScroll = new ViewScroll();
            StationsScroll.setRelativeTo(Window);
            StationsScroll.setWidth(Window.getWidth() - 140);
            StationsScroll.setHeight(Window.getHeight() - 160);
            StationsScroll.setLeft(140);
            StationsScroll.setTop(150);

            Vw.addComponent(StationsScroll);

            SpaceStationReport[] Stations = review.variables.SpaceStations;

            for (int i = 0; i < Stations.Length; i++)
            {
                drawItem(Stations[i], StationsScroll, i);
            }

        }

        public static void drawItem(SpaceStationReport Station, ViewScroll parent, int offset)
        {
            int boxHeight = 110;

            ViewBox Box = new ViewBox();
            Box.setRelativeTo(parent);
            Box.setWidth(parent.getWidth() - 20);
            Box.setHeight(boxHeight);
            Box.setLeft(0);
            Box.setTop((boxHeight + 10) * offset);
            Box.setColor(Color.white);
            parent.Components.Add(Box);

            string label = "[" + Station.name + Localizer.Format("#LOC_StateFunding_is_Orbiting") + " " + Station.entity + "]";
            ViewLabel StationLabel = new ViewLabel(label);
            StationLabel.setRelativeTo(Box);
            StationLabel.setTop(5);
            StationLabel.setLeft(5);
            StationLabel.setHeight(15);
            StationLabel.setPercentWidth(100);
            StationLabel.setColor(Color.green);
            parent.Components.Add(StationLabel);

            ViewLabel FuelLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Fuel") + " " + Station.fuel);
            FuelLabel.setRelativeTo(Box);
            FuelLabel.setTop(25);
            FuelLabel.setLeft(5);
            FuelLabel.setHeight(15);
            FuelLabel.setWidth(150);
            FuelLabel.setColor(Color.white);
            parent.Components.Add(FuelLabel);

            ViewLabel OreLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Ore") + " " + Station.ore);
            OreLabel.setRelativeTo(Box);
            OreLabel.setTop(45);
            OreLabel.setLeft(5);
            OreLabel.setHeight(20);
            OreLabel.setWidth(150);
            OreLabel.setColor(Color.white);
            parent.Components.Add(OreLabel);

            ViewLabel CrewLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Crew") + " " + Station.crew);
            CrewLabel.setRelativeTo(Box);
            CrewLabel.setTop(65);
            CrewLabel.setLeft(5);
            CrewLabel.setHeight(20);
            CrewLabel.setWidth(150);
            CrewLabel.setColor(Color.white);
            parent.Components.Add(CrewLabel);

            ViewLabel CrewCapacityLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Crew_Capacity") + " " + Station.crewCapacity);
            CrewCapacityLabel.setRelativeTo(Box);
            CrewCapacityLabel.setTop(85);
            CrewCapacityLabel.setLeft(5);
            CrewCapacityLabel.setHeight(20);
            CrewCapacityLabel.setWidth(150);
            CrewCapacityLabel.setColor(Color.white);
            parent.Components.Add(CrewCapacityLabel);

            ViewLabel DockingPortsLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Docking_Ports") + " " + Station.dockingPorts);
            DockingPortsLabel.setRelativeTo(Box);
            DockingPortsLabel.setTop(25);
            DockingPortsLabel.setLeft(155);
            DockingPortsLabel.setHeight(15);
            DockingPortsLabel.setWidth(150);
            DockingPortsLabel.setColor(Color.white);
            parent.Components.Add(DockingPortsLabel);

            ViewLabel DockedVesselsLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Docked_Vessels") + " " + Station.dockedVessels);
            DockedVesselsLabel.setRelativeTo(Box);
            DockedVesselsLabel.setTop(45);
            DockedVesselsLabel.setLeft(155);
            DockedVesselsLabel.setHeight(15);
            DockedVesselsLabel.setWidth(150);
            DockedVesselsLabel.setColor(Color.white);
            parent.Components.Add(DockedVesselsLabel);

            ViewLabel ScienceLabLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Science_Lab") + " " + Station.scienceLab);
            ScienceLabLabel.setRelativeTo(Box);
            ScienceLabLabel.setTop(65);
            ScienceLabLabel.setLeft(155);
            ScienceLabLabel.setHeight(15);
            ScienceLabLabel.setWidth(150);
            ScienceLabLabel.setColor(Color.white);
            parent.Components.Add(ScienceLabLabel);

            ViewLabel HasDrillLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Has_Drill") + " " + Station.drill);
            HasDrillLabel.setRelativeTo(Box);
            HasDrillLabel.setTop(85);
            HasDrillLabel.setLeft(155);
            HasDrillLabel.setHeight(15);
            HasDrillLabel.setWidth(150);
            HasDrillLabel.setColor(Color.white);
            parent.Components.Add(HasDrillLabel);

            ViewLabel AsteroidLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_On_Asteroid") + " " + Station.onAsteroid);
            AsteroidLabel.setRelativeTo(Box);
            AsteroidLabel.setTop(25);
            AsteroidLabel.setLeft(310);
            AsteroidLabel.setHeight(15);
            AsteroidLabel.setWidth(150);
            AsteroidLabel.setColor(Color.white);
            parent.Components.Add(AsteroidLabel);

            ViewLabel SCLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_PO") + " " + Station.po);
            SCLabel.setRelativeTo(Box);
            SCLabel.setTop(45);
            SCLabel.setLeft(310);
            SCLabel.setHeight(15);
            SCLabel.setWidth(150);
            SCLabel.setColor(Color.white);
            parent.Components.Add(SCLabel);

            ViewLabel POLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_SC") + " " + Station.sc);
            POLabel.setRelativeTo(Box);
            POLabel.setTop(65);
            POLabel.setLeft(310);
            POLabel.setHeight(15);
            POLabel.setWidth(150);
            POLabel.setColor(Color.white);
            parent.Components.Add(POLabel);
        }

    }
}

