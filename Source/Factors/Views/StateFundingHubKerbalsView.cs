using KSP.Localization;
using StateFunding.Factors.Views;
using UnityEngine;

namespace StateFunding.Factors
{
    public class StateFundingHubKerbalsView : IFactorView
    {
        public string getSideMenuText()
        {
            return Localizer.Format("#LOC_StateFunding_Kerbals");
        }

        public void draw(View Vw, ViewWindow Window, Review review)
        {
            Window.title = Localizer.Format("#LOC_StateFunding_Kerbals");

            InstanceData GameInstance = StateFundingGlobal.fetch.GameInstance;
            if (GameInstance == null)
            {
                Log.Error("StateFundingHubKerbalsView.draw, Inst is null");
                return;
            }

            string Description = Localizer.Format("#LOC_StateFunding_You_Love_Kerbals_I_Love_K") +
              Localizer.Format("#LOC_StateFunding_So_it_goes_without_saying") +
              Localizer.Format("#LOC_StateFunding_The_more_Kerbals_you_have") +
              Localizer.Format("#LOC_StateFunding_a_stranded_Kerbal_is_as_b") +
              Localizer.Format("#LOC_StateFunding_rescued_A_qualified_Stran") +
              Localizer.Format("#LOC_StateFunding_or_a_mining_rig_They_are_") +
              Localizer.Format("#LOC_StateFunding_been_on_the_current_missi");

            ViewLabel DescriptionLabel = new ViewLabel(Description);
            DescriptionLabel.setRelativeTo(Window);
            DescriptionLabel.setLeft(140);
            DescriptionLabel.setTop(20);
            DescriptionLabel.setColor(Color.white);
            DescriptionLabel.setHeight(100);
            DescriptionLabel.setWidth(Window.getWidth() - 140);

            Vw.addComponent(DescriptionLabel);

            ViewLabel ActiveKerbals = new ViewLabel(Localizer.Format("#LOC_StateFunding_Active_Kerbals") + " " + review.variables.activeKerbals +
                Localizer.Format("#LOC_StateFunding_Stranded_Kerbals_DUP1") + " " + review.variables.strandedKerbals + ".");
            ActiveKerbals.setRelativeTo(Window);
            ActiveKerbals.setLeft(140);
            ActiveKerbals.setTop(130);
            ActiveKerbals.setColor(Color.white);
            ActiveKerbals.setHeight(30);
            ActiveKerbals.setWidth(Window.getWidth() - 140);

            Vw.addComponent(ActiveKerbals);

            ViewScroll KerbalsScroll = new ViewScroll();
            KerbalsScroll.setRelativeTo(Window);
            KerbalsScroll.setWidth(Window.getWidth() - 140);
            KerbalsScroll.setHeight(Window.getHeight() - 160);
            KerbalsScroll.setLeft(140);
            KerbalsScroll.setTop(150);

            Vw.addComponent(KerbalsScroll);

            ProtoCrewMember[] Kerbals = KerbalHelper.GetKerbals();

            int labelHeight = 20;

            for (int i = 0; i < Kerbals.Length; i++)
            {
                ProtoCrewMember Kerb = Kerbals[i];

                string state = Localizer.Format("#LOC_StateFunding_Active");
                Color color = Color.green;
                if (KerbalHelper.IsStranded(Kerb))
                {
                    state = Localizer.Format("#LOC_StateFunding_Stranded");
                    color = Color.white;
                }
                else if (KerbalHelper.QualifiedStranded(Kerb))
                {
                    state = Localizer.Format("#LOC_StateFunding_Active_Will_be_Stranded_I") + KerbalHelper.TimeToStranded(Kerb) + Localizer.Format("#LOC_StateFunding_Days");
                    color = Color.yellow;
                }

                string label = Kerb.name + " (" + state + ")";

                ViewLabel KerbalLabel = new ViewLabel(label);
                KerbalLabel.setRelativeTo(KerbalsScroll);
                KerbalLabel.setTop(labelHeight + (labelHeight + 5) * i);
                KerbalLabel.setLeft(0);
                KerbalLabel.setHeight(labelHeight);
                KerbalLabel.setWidth(KerbalsScroll.getWidth() - 20);
                KerbalLabel.setColor(color);
                KerbalsScroll.Components.Add(KerbalLabel);
            }

        }
    }
}

