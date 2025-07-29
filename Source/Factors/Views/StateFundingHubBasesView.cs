using KSP.Localization;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StateFunding.Factors.Views;

namespace StateFunding.Factors.Views
{
    public class StateFundingHubBasesView: IFactorView
    {
        public string getSideMenuText()
        {
            return Localizer.Format("#LOC_StateFunding_Bases_DUP1");
        }

        public void draw(View Vw, ViewWindow Window, Review review)
        {
            Window.title = Localizer.Format("#LOC_StateFunding_Bases_DUP1");
            InstanceData GameInstance = StateFundingGlobal.fetch.GameInstance;
            if (GameInstance == null)
            {
                Log.Error("StateFundingHubBasesView.draw, Inst is null");
                return;
            }

            string Description = Localizer.Format("#LOC_StateFunding_Below_is_a_list_of_existi") +
                                 Localizer.Format("#LOC_StateFunding_such_be_landed_on_a_body_") +
                                 Localizer.Format("#LOC_StateFunding_as_well_as_Public_Opinion") +
                                 Localizer.Format("#LOC_StateFunding_SC_Docking_Port_Count_SC_");

            ViewLabel DescriptionLabel = new ViewLabel(Description);
            DescriptionLabel.setRelativeTo(Window);
            DescriptionLabel.setLeft(140);
            DescriptionLabel.setTop(20);
            DescriptionLabel.setColor(Color.white);
            DescriptionLabel.setHeight(100);
            DescriptionLabel.setWidth(Window.getWidth() - 140);

            Vw.addComponent(DescriptionLabel);

            ViewLabel TotalBases = new ViewLabel(Localizer.Format("#LOC_StateFunding_Total_Bases") + " " + review.variables.Bases.Length);
            TotalBases.setRelativeTo(Window);
            TotalBases.setLeft(140);
            TotalBases.setTop(130);
            TotalBases.setColor(Color.white);
            TotalBases.setHeight(30);
            TotalBases.setWidth(Window.getWidth() - 140);

            Vw.addComponent(TotalBases);

            ViewScroll BasesScroll = new ViewScroll();
            BasesScroll.setRelativeTo(Window);
            BasesScroll.setWidth(Window.getWidth() - 140);
            BasesScroll.setHeight(Window.getHeight() - 160);
            BasesScroll.setLeft(140);
            BasesScroll.setTop(150);

            Vw.addComponent(BasesScroll);

            BaseReport[] Bases = review.variables.Bases;

            for (int i = 0; i < Bases.Length; i++)
            {
                drawItem(Bases[i], BasesScroll, i);
            }

        }

        public static void drawItem(BaseReport Base, ViewScroll parent, int offset)
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

            string label = "[" + Base.name + Localizer.Format("#LOC_StateFunding_is_Landed_At") + " " + Base.entity + "]";
            ViewLabel BaseLabel = new ViewLabel(label);
            BaseLabel.setRelativeTo(Box);
            BaseLabel.setTop(5);
            BaseLabel.setLeft(5);
            BaseLabel.setHeight(15);
            BaseLabel.setPercentWidth(100);
            BaseLabel.setColor(Color.green);
            parent.Components.Add(BaseLabel);

            ViewLabel FuelLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Fuel") + " " + Base.fuel);
            FuelLabel.setRelativeTo(Box);
            FuelLabel.setTop(25);
            FuelLabel.setLeft(5);
            FuelLabel.setHeight(15);
            FuelLabel.setWidth(150);
            FuelLabel.setColor(Color.white);
            parent.Components.Add(FuelLabel);

            ViewLabel OreLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Ore") + " " + Base.ore);
            OreLabel.setRelativeTo(Box);
            OreLabel.setTop(45);
            OreLabel.setLeft(5);
            OreLabel.setHeight(20);
            OreLabel.setWidth(150);
            OreLabel.setColor(Color.white);
            parent.Components.Add(OreLabel);

            ViewLabel CrewLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Crew") + " " + Base.crew);
            CrewLabel.setRelativeTo(Box);
            CrewLabel.setTop(65);
            CrewLabel.setLeft(5);
            CrewLabel.setHeight(20);
            CrewLabel.setWidth(150);
            CrewLabel.setColor(Color.white);
            parent.Components.Add(CrewLabel);

            ViewLabel CrewCapacityLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Crew_Capacity") + " " + Base.crewCapacity);
            CrewCapacityLabel.setRelativeTo(Box);
            CrewCapacityLabel.setTop(85);
            CrewCapacityLabel.setLeft(5);
            CrewCapacityLabel.setHeight(20);
            CrewCapacityLabel.setWidth(150);
            CrewCapacityLabel.setColor(Color.white);
            parent.Components.Add(CrewCapacityLabel);

            ViewLabel DockingPortsLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Docking_Ports") + " " + Base.dockingPorts);
            DockingPortsLabel.setRelativeTo(Box);
            DockingPortsLabel.setTop(25);
            DockingPortsLabel.setLeft(155);
            DockingPortsLabel.setHeight(15);
            DockingPortsLabel.setWidth(150);
            DockingPortsLabel.setColor(Color.white);
            parent.Components.Add(DockingPortsLabel);

            ViewLabel DockedVesselsLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Docked_Vessels") + " " + Base.dockedVessels);
            DockedVesselsLabel.setRelativeTo(Box);
            DockedVesselsLabel.setTop(45);
            DockedVesselsLabel.setLeft(155);
            DockedVesselsLabel.setHeight(15);
            DockedVesselsLabel.setWidth(150);
            DockedVesselsLabel.setColor(Color.white);
            parent.Components.Add(DockedVesselsLabel);

            ViewLabel ScienceLabLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Science_Lab") + " " + Base.scienceLab);
            ScienceLabLabel.setRelativeTo(Box);
            ScienceLabLabel.setTop(65);
            ScienceLabLabel.setLeft(155);
            ScienceLabLabel.setHeight(15);
            ScienceLabLabel.setWidth(150);
            ScienceLabLabel.setColor(Color.white);
            parent.Components.Add(ScienceLabLabel);

            ViewLabel HasDrillLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_Has_Drill") + " " + Base.drill);
            HasDrillLabel.setRelativeTo(Box);
            HasDrillLabel.setTop(85);
            HasDrillLabel.setLeft(155);
            HasDrillLabel.setHeight(15);
            HasDrillLabel.setWidth(150);
            HasDrillLabel.setColor(Color.white);
            parent.Components.Add(HasDrillLabel);

            ViewLabel SCLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_PO") + " " + Base.po);
            SCLabel.setRelativeTo(Box);
            SCLabel.setTop(25);
            SCLabel.setLeft(310);
            SCLabel.setHeight(15);
            SCLabel.setWidth(150);
            SCLabel.setColor(Color.white);
            parent.Components.Add(SCLabel);

            ViewLabel POLabel = new ViewLabel(Localizer.Format("#LOC_StateFunding_SC") + " " + Base.sc);
            POLabel.setRelativeTo(Box);
            POLabel.setTop(45);
            POLabel.setLeft(310);
            POLabel.setHeight(15);
            POLabel.setWidth(150);
            POLabel.setColor(Color.white);
            parent.Components.Add(POLabel);
        }
    }
}

