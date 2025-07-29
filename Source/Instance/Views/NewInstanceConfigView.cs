using KSP.Localization;
using System;
using UnityEngine;
using System.Collections;

namespace StateFunding
{
    public class NewInstanceConfigView : View
    {
        private ViewWindow Window;
        private ViewImage Image;
        private ViewLabel Label;
        private ViewButton Confirm;
        private ViewLabel GovernmentDescription;
        private ViewLabel GovernmentGameplayDescription;
        private Government SelectedGovernment;
        private Action<InstanceData> OnCreateCallback;

        public NewInstanceConfigView()
        {
            Log.Info("StateFunding: NewInstanceConfigView: creating"); // NO_LOCALIZATION
            ViewManager.addView(this);
            createWindow();
            createGovernmentMenu();
            CreateGovernmentView();
            SelectGovernment((Government)StateFundingGlobal.fetch.Governments.ToArray()[0]);
        }

        private void createWindow()
        {
            Log.Info("StateFunding: NewInstanceConfigView: creating window"); // NO_LOCALIZATION
            Window = new ViewWindow(Localizer.Format("#LOC_StateFunding_State_Funding"));
            Window.setMargins(300, 100);

            Image = new ViewImage("assets/kerbalgovernment.jpg"); // NO_LOCALIZATION
            Image.setRelativeTo(Window);
            Image.setPercentWidth(100);


            Label = new ViewLabel(
              Localizer.Format("#LOC_StateFunding_We_ve_been_told_it_was_in") +
              Localizer.Format("#LOC_StateFunding_We_re_not_rocket_scientis") +
              Localizer.Format("#LOC_StateFunding_Or_at_least_just_help_us_")
            );
            Label.setRelativeTo(Image);
            Label.setPercentWidth(80);
            Label.setPercentHeight(20);
            Label.setPercentLeft(10);
            Label.setPercentTop(80);
            Label.setFontSize(18);
            Label.setColor(Color.white);

            Confirm = new ViewButton(Localizer.Format("#LOC_StateFunding_Ok"), OnConfirm);
            Confirm.setRelativeTo(Window);
            Confirm.setWidth(150);
            Confirm.setHeight(30);
            Confirm.setRight(5);
            Confirm.setBottom(5);

            this.addComponent(Window);
            this.addComponent(Image);
            this.addComponent(Label);
            this.addComponent(Confirm);
        }
        void CreateGovernmentView()
        {
            int GvtDescrLeft = 120;
            int GvtGameplayWidth = 250;
            int Availablewidth = Window.getWidth() - GvtDescrLeft - GvtGameplayWidth -40;

            int GvtDescrWidth = Availablewidth; // 500;
            int GvtGameplayLeft = Window.getWidth() - GvtGameplayWidth; // 640;
           

            GvtDescrLeft += GvtMenuAdjust;
            GvtGameplayLeft += GvtMenuAdjust;

            #region  NO_LOCALIZATION
            Log.Info("GvtMenuAdjust: " + GvtMenuAdjust);
            Log.Info("GvtDescrLeft: " + GvtDescrLeft);
            Log.Info("GvtGameplayWidth: " + GvtGameplayWidth);
            Log.Info("GvtDescrWidth: " + GvtDescrWidth);
            Log.Info("GvtGameplayLeft: " + GvtGameplayLeft);
            #endregion

            GovernmentDescription = new ViewLabel("");
            GovernmentDescription.setRelativeTo(Image);
            GovernmentDescription.setWidth(GvtDescrWidth);
            GovernmentDescription.setHeight(Window.getHeight() - Image.getHeight() - 20);
            GovernmentDescription.setTop(Image.getHeight() + 10);
            GovernmentDescription.setLeft(GvtDescrLeft);
            GovernmentDescription.setColor(Color.white);
            GovernmentDescription.setFontSize(14);

            GovernmentGameplayDescription = new ViewLabel("");
            GovernmentGameplayDescription.setRelativeTo(Image);
            GovernmentGameplayDescription.setWidth(GvtGameplayWidth);
            GovernmentGameplayDescription.setHeight(Window.getHeight() - Image.getHeight() - 20);
            GovernmentGameplayDescription.setTop(Image.getHeight() + 10);
            GovernmentGameplayDescription.setLeft(GvtGameplayLeft);
            GovernmentGameplayDescription.setColor(Color.white);
            GovernmentGameplayDescription.setFontSize(14);            
    
            this.addComponent(GovernmentDescription);
            this.addComponent(GovernmentGameplayDescription);
 
        }



        int GvtMenuAdjust = 0;
        
        private void createGovernmentMenu()
        {
            Log.Info("StateFunding: NewInstanceConfigView: creating government menu"); // NO_LOCALIZATION
            int buttonHeight = 30; // Includes space between buttons
            int top = Image.getHeight() + 10;
            int bottom = Window.getHeight() - 2 * buttonHeight;
            int bpos = 0;
            int badjust = 0;
            int left = 10;
            for (int i = 0; i < StateFundingGlobal.fetch.Governments.ToArray().Length; i++)
            {
                Government Gov = (Government)StateFundingGlobal.fetch.Governments.ToArray()[i];
                ViewGovernmentButton GovBtn = new ViewGovernmentButton(Gov, SelectGovernment);
                GovBtn.setRelativeTo(Image);
                GovBtn.setWidth(100);
                GovBtn.setHeight(buttonHeight - 5);
                bpos = 30 * i - badjust;
                if (top +bpos > bottom)
                {
                    badjust = 30 * i;
                    bpos = 0;
                    left += 110;
                }

                GovBtn.setTopAndLeft(top + bpos, left);
                //GovBtn.setTop(top + bpos);
                //GovBtn.setLeft(left);

                this.addComponent(GovBtn);
            }
            GvtMenuAdjust = left - 10;
          //  SelectGovernment((Government)StateFundingGlobal.fetch.Governments.ToArray()[0]);
        }

        private void SelectGovernment(Government Gov)
        {
            SelectedGovernment = Gov;
            GovernmentDescription.label = Gov.description;
            GovernmentGameplayDescription.label = Gov.GetGameplayDescription();
            Confirm.text = Localizer.Format("#LOC_StateFunding_Select")+" " + Gov.name;
            Log.Info("StateFunding: NewInstanceConfigView: selected government"); // NO_LOCALIZATION
        }

        private void OnConfirm()
        {
            InstanceData Inst = InstanceData.getInstance();
            Inst.Gov = SelectedGovernment;
            Inst.govName = SelectedGovernment.name;
            Inst.po = (int)SelectedGovernment.startingPO;
            Inst.sc = (int)SelectedGovernment.startingSC;
            ViewManager.removeView(this);
            OnCreateCallback(Inst);
        }

        public void OnCreate(Action<InstanceData> Callback)
        {
            Log.Info("StateFunding: NewInstanceConfigView: calling callback");
            OnCreateCallback = Callback;
        }
    }
}

