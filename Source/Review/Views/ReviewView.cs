using KSP.Localization;
using System;
using UnityEngine;
using System.Collections;
using StateFunding.ViewComponents;
using System.Collections.Generic;

namespace StateFunding
{
    public class ReviewView : View
    {
        private ViewWindow Window;
        private ViewImage Image;
        private ViewLabel Label;
        private ViewTextArea ReviewText;
        private ViewButton Confirm;
        private Review Rev;

        public ReviewView(Review Rev)
        {
            ViewManager.addView(this);
            this.Rev = Rev;
            createWindow();
        }

        private void createWindow()
        {
            Window = new ViewWindow(Localizer.Format("#LOC_StateFunding_Review"));
            Window.setMargins(300, 100);

            Image = new ViewImage("assets/kerbalfunding.jpg"); // NO_LOCALIZATION
            Image.setRelativeTo(Window);
            Image.setPercentWidth(100);

            Label = new ViewLabel(Localizer.Format("#LOC_StateFunding_Could_be_worse"));
            Label.setRelativeTo(Image);
            Label.setPercentWidth(80);
            Label.setPercentHeight(20);
            Label.setPercentLeft(10);
            Label.setPercentTop(80);
            Label.setFontSize(18);
            Label.setColor(Color.white);

            Confirm = new ViewButton(Localizer.Format("#LOC_StateFunding_Ok_DUP1"), OnConfirm);
            Confirm.setRelativeTo(Window);
            Confirm.setWidth(100);
            Confirm.setHeight(30);
            Confirm.setRight(5);
            Confirm.setBottom(5);

            if (!Rev.pastReview)
            {
                Rev.touch();
            }

            ReviewText = new ViewTextArea(Rev.GetSummaryText());
            ReviewText.setRelativeTo(Image);
            ReviewText.setPercentWidth(100);
            ReviewText.setTop(Image.getHeight() + 10);
            ReviewText.setHeight(Window.getHeight() - Image.getHeight() - Confirm.getHeight() - 20);
            ReviewText.setColor(Color.white);

            this.addComponent(Window);
            this.addComponent(Image);
            this.addComponent(Label);
            this.addComponent(Confirm);
            this.addComponent(ReviewText);

            List<ViewSummaryRow> summaryRows = Rev.GetText();
            for (int i=0; i < summaryRows.Count; i++)
            {
                ViewSummaryRow row = summaryRows[i];
                row.setRelativeTo(ReviewText);
                row.setHeight(30);
                row.setPercentWidth(100);
                row.setTop(15 * i + 10);
                row.setLeft(300);
                row.setColor(Color.white);
                this.addComponent(row);
            }
        }

        private void OnConfirm()
        {
            ViewManager.removeView(this);
            StateFundingApplicationLauncher.toolbarControl.SetFalse();
        }
    }
}

