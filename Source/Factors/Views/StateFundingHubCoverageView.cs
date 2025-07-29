using KSP.Localization;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StateFunding.Factors.Views;
using StateFunding.Factors;

namespace StateFunding
{
    public class StateFundingHubCoverageView : IFactorView
    {
        public string getSideMenuText()
        {
            return Localizer.Format("#LOC_StateFunding_Sat_Coverage");
        }

        public void draw(View Vw, ViewWindow Window, Review review)
        {
            Window.title = Localizer.Format("#LOC_StateFunding_Satellite_Coverage_DUP1");

            InstanceData GameInstance = StateFundingGlobal.fetch.GameInstance;
            if (GameInstance == null)
            {
                Log.Error("StateFundingHubCoverageView.draw, Inst is null");
                return;
            }

            string Description = Localizer.Format("#LOC_StateFunding_Below_is_your_space_progr") +
              Localizer.Format("#LOC_StateFunding_The_number_of_satellites_") +
              Localizer.Format("#LOC_StateFunding_celestial_body_Kerbin_nee") +
              Localizer.Format("#LOC_StateFunding_needs_1_and_the_massive_J") +
              Localizer.Format("#LOC_StateFunding_to_all_celestial_bodies_S") +
              Localizer.Format("#LOC_StateFunding_rating_by_covering_the_sm") +
              Localizer.Format("#LOC_StateFunding_a_qualified_Surveyor_Sate") +
              Localizer.Format("#LOC_StateFunding_able_to_generate_power");

            ViewLabel DescriptionLabel = new ViewLabel(Description);
            DescriptionLabel.setRelativeTo(Window);
            DescriptionLabel.setLeft(140);
            DescriptionLabel.setTop(20);
            DescriptionLabel.setColor(Color.white);
            DescriptionLabel.setHeight(100);
            DescriptionLabel.setWidth(Window.getWidth() - 140);

            Vw.addComponent(DescriptionLabel);

            ViewLabel TotalCoverage = new ViewLabel(Localizer.Format("#LOC_StateFunding_Total_Coverage") + " " + Math.Round((double)review.variables.satelliteCoverage * 100) + "%");
            TotalCoverage.setRelativeTo(Window);
            TotalCoverage.setLeft(140);
            TotalCoverage.setTop(130);
            TotalCoverage.setColor(Color.white);
            TotalCoverage.setHeight(30);
            TotalCoverage.setWidth(Window.getWidth() - 140);

            Vw.addComponent(TotalCoverage);

            ViewScroll CoverageScroll = new ViewScroll();
            CoverageScroll.setRelativeTo(Window);
            CoverageScroll.setWidth(Window.getWidth() - 140);
            CoverageScroll.setHeight(Window.getHeight() - 160);
            CoverageScroll.setLeft(140);
            CoverageScroll.setTop(150);

            Vw.addComponent(CoverageScroll);

            CoverageReport[] Coverages = review.variables.Coverages;

            int labelHeight = 20;

            for (int i = 0; i < Coverages.Length; i++)
            {
                CoverageReport Coverage = Coverages[i];
                string label = Coverage.entity + " : (" +
                  Coverage.satCount + "/" +
                  Coverage.satCountForFullCoverage + ") " +
                  Math.Round(Coverage.coverage * 100) + "%";

                ViewLabel CoverageLabel = new ViewLabel(label);
                CoverageLabel.setRelativeTo(CoverageScroll);
                CoverageLabel.setTop(labelHeight + (labelHeight + 5) * i);
                CoverageLabel.setLeft(0);
                CoverageLabel.setHeight(labelHeight);
                CoverageLabel.setWidth(CoverageScroll.getWidth() - 20);

                if (Coverage.coverage <= 0.25)
                {
                    CoverageLabel.setColor(Color.white);
                }
                else if (Coverage.coverage <= .75)
                {
                    CoverageLabel.setColor(Color.yellow);
                }
                else
                {
                    CoverageLabel.setColor(Color.green);
                }

                CoverageScroll.Components.Add(CoverageLabel);
            }
        }
    }
}

