using KSP.Localization;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StateFunding.ViewComponents;

namespace StateFunding
{
    public static class StateFundingHubCurrentView
    {
        public static void draw(View Vw, ViewWindow Window)
        {
            Window.title = Localizer.Format("#LOC_StateFunding_Current_State");
            InstanceData GameInstance = StateFundingGlobal.fetch.GameInstance;
            if (GameInstance == null)
            {
                Log.Error("StateFundingHubCurrentView.draw, Inst is null");
                return;
            }

            Review Rev = GameInstance.ActiveReview;
            Rev.touch();
            ViewTextArea TextArea = new ViewTextArea(GameInstance.ActiveReview.GetSummaryText());
            TextArea.setRelativeTo(Window);
            TextArea.setTop(40);
            TextArea.setLeft(130);
            TextArea.setWidth(Window.getWidth() - 140);
            TextArea.setHeight(Window.getHeight() - 40);
            TextArea.setColor(Color.white);

            Vw.addComponent(TextArea);

            List<ViewSummaryRow> summaryRows = Rev.GetText();
            for (int i = 0; i < summaryRows.Count; i++)
            {
                ViewSummaryRow row = summaryRows[i];
                row.setRelativeTo(TextArea);
                row.setHeight(30);
                row.setPercentWidth(100);
                row.setTop(200 + 20 * i + 10);
                row.setColor(Color.white);
                Vw.addComponent(row);
            }
        }
    }
}

