using System;
using UnityEngine;
using System.Collections;
using StateFunding.Factors;

namespace StateFunding
{
    public class StateFundingHubView : View
    {
        private ViewWindow Window;
        private ArrayList SideMenu;

        public InstanceData GameInstance
        {
            get
            {
                if (StateFundingScenario.Instance != null)
                    return StateFundingScenario.Instance.Data;
                return null;
            }
        }

        public StateFundingHubView()
        {
            createWindow();
        }

        private void createWindow()
        {
            reloadBase();
        }

        private void reloadBase()
        {
            this.removeAll();

            SideMenu = new ArrayList();

            Window = new ViewWindow("");
            Window.setWidth(800);
            Window.setHeight(Screen.height - 200);
            Window.setLeft((Screen.width - 800) / 2);
            Window.setTop(100);

            this.addComponent(Window);
            SideMenu.Add(new ViewButton("Current State", LoadCurrentState));
            foreach (Factor factor in GameInstance.ActiveReview.factors)
            {
                if (factor.View != null)
                {
                    SideMenu.Add(new ViewButton(factor.View.getSideMenuText(), LoadView(factor, GameInstance.ActiveReview)));
                }
            }
            SideMenu.Add(new ViewButton("Past Reviews", LoadPastReviews));

            for (var i = 0; i < SideMenu.ToArray().Length; i++)
            {
                ViewButton Btn = (ViewButton)SideMenu.ToArray()[i];
                Btn.setRelativeTo(Window);
                Btn.setLeft(10);
                Btn.setTop(10 + i * 45);
                Btn.setWidth(120);
                Btn.setHeight(35);
                this.addComponent(Btn);
            }
        }

        private Action LoadView(Factor factor, Review review)
        {
            return () =>
            {
                reloadBase();
                factor.View.draw(this, Window, review);
            };
        }

        private void LoadCurrentState()
        {
            reloadBase();
            StateFundingHubCurrentView.draw(this, Window);
        }

        private void LoadPastReviews()
        {
            reloadBase();
            StateFundingHubReviewsView.draw(this, Window);
        }
    }
}

