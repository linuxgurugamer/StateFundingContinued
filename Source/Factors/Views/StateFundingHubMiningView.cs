using KSP.Localization;
using UnityEngine;
using StateFunding.Factors.Views;
using StateFunding.Factors;

namespace StateFunding
{
    public class StateFundingHubMiningView : IFactorView
    {
        public string getSideMenuText()
        {
            return Localizer.Format("#LOC_StateFunding_Mining_Rigs");
        }

        public void draw(View Vw, ViewWindow Window, Review review)
        {
            Window.title = Localizer.Format("#LOC_StateFunding_Mining_Rigs");

            InstanceData GameInstance = StateFundingGlobal.fetch.GameInstance;
            if (GameInstance == null)
            {
                Log.Error("StateFundingHubMiningView.draw, Inst is null");
                return;
            }

            string Description = Localizer.Format("#LOC_StateFunding_Below_is_a_list_of_existi_DUP1") +
              Localizer.Format("#LOC_StateFunding_Confidence_To_have_a_qual") +
              Localizer.Format("#LOC_StateFunding_and_be_Landed_on_a_body_o");

            ViewLabel DescriptionLabel = new ViewLabel(Description);
            DescriptionLabel.setRelativeTo(Window);
            DescriptionLabel.setLeft(140);
            DescriptionLabel.setTop(20);
            DescriptionLabel.setColor(Color.white);
            DescriptionLabel.setHeight(100);
            DescriptionLabel.setWidth(Window.getWidth() - 140);

            Vw.addComponent(DescriptionLabel);

            ViewLabel TotalCoverage = new ViewLabel(Localizer.Format("#LOC_StateFunding_Mining_Rigs_DUP1") + " " + review.variables.miningRigs);
            TotalCoverage.setRelativeTo(Window);
            TotalCoverage.setLeft(140);
            TotalCoverage.setTop(130);
            TotalCoverage.setColor(Color.white);
            TotalCoverage.setHeight(30);
            TotalCoverage.setWidth(Window.getWidth() - 140);

            Vw.addComponent(TotalCoverage);

            ViewScroll RigsScroll = new ViewScroll();
            RigsScroll.setRelativeTo(Window);
            RigsScroll.setWidth(Window.getWidth() - 140);
            RigsScroll.setHeight(Window.getHeight() - 160);
            RigsScroll.setLeft(140);
            RigsScroll.setTop(150);

            Vw.addComponent(RigsScroll);

            Vessel[] MiningRigs = VesselHelper.GetMiningRigs();

            int labelHeight = 20;

            for (int i = 0; i < MiningRigs.Length; i++)
            {
                Vessel MiningRig = MiningRigs[i];

                string label = MiningRig.GetName() + Localizer.Format("#LOC_StateFunding_is_Landed_At") + " " + MiningRig.mainBody.GetName(); ;

                ViewLabel MiningLabel = new ViewLabel(label);
                MiningLabel.setRelativeTo(RigsScroll);
                MiningLabel.setTop(labelHeight + (labelHeight + 5) * i);
                MiningLabel.setLeft(0);
                MiningLabel.setHeight(labelHeight);
                MiningLabel.setWidth(RigsScroll.getWidth() - 20);
                MiningLabel.setColor(Color.white);

                RigsScroll.Components.Add(MiningLabel);
            }
        }
    }
}

