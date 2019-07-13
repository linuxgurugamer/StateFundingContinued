using System;
using UnityEngine;
using System.Collections;
using StateFunding.Factors;
using System.Collections.Generic;

namespace StateFunding
{
    public class Review
    {
        public Review()
        {
            variables = new FactorVariables();
            factors = new Factor[] {
                new KerbalsFactor(variables),
                new SatelliteCoverageFactor(variables),
                new MiningRigsFactor(variables),
                new RoversFactor(variables),
                new ScienceStationsFactor(variables),
                new ContractsFactor(variables),
                new SpaceStationsFactor(variables),
                new BasesFactor(variables),
            };
        }

        public Factor[] factors;

        [Persistent]
        public FactorVariables variables;

        [Persistent]
        public int finalPO = 0;

        [Persistent]
        public int finalSC = 0;

        [Persistent]
        public int funds = 0;
        
        [Persistent]
        public int vesselsDestroyed = 0;

        [Persistent]
        public int po = 0;

        [Persistent]
        public bool pastReview = false;
        
        [Persistent]
        public int sc = 0;
        
        [Persistent]
        public int year = 0;

        private void UpdatePOSC()
        {
            Log.Info("Updating POSC");
            InstanceData GameInstance = StateFundingGlobal.fetch.GameInstance;
            if (GameInstance == null)
            {
                Log.Error("Review.UpdatePOSC, GameInstance is null");
                return;
            }

            po = GameInstance.po;
            sc = GameInstance.sc;
        }

        public void closeReview()
        {
            pastReview = true;

            foreach (Factor factor in factors)
            {
                factor.cleanup();
            }
        }

        public void touch()
        {
            if (!pastReview)
            {
                UpdatePOSC();

                foreach (Factor factor in factors)
                {
                    factor.Update();
                }
                UpdateFinalPO();
                UpdateFinalSC();
                UpdateFunds();
                UpdateYear();
            }
            else
            {
                Log.Error("Cannot touch a past review. It's properties are already set");
            }
        }

        public void UpdateYear()
        {
            Log.Info("Updating Year");
            //year = TimeHelper.Quarters(Planetarium.GetUniversalTime());
            year = TimeHelper.Periods(Planetarium.GetUniversalTime(), HighLogic.CurrentGame.Parameters.CustomParams<StateFundingSettings>().budgetPeriodsPerYear);
        }

        public void UpdateFinalPO()
        {
            Log.Info("Updating Final PO");
            int tmpPO = po;

            InstanceData Inst = StateFundingGlobal.fetch.GameInstance;
            if (Inst == null)
            {
                Log.Error("Review.UpdateFinalPO, GameInstance is null");
                return;
            }

            foreach (Factor factor in factors)
            {
                tmpPO += factor.modPO;
            }

            finalPO = tmpPO;
        }

        public void UpdateFinalSC()
        {
            Log.Info("Updating Final SC");
            int tmpSC = sc;

            InstanceData Inst = StateFundingGlobal.fetch.GameInstance;
            if (Inst == null)
            {
                Log.Error("Review.UpdateFinalSC, GameInstance is null");
                return;
            }

            foreach (Factor factor in factors)
            {
                tmpSC += factor.modSC;
            }

            finalSC = tmpSC;
        }

        private void UpdateFunds()
        {
            Log.Info("Updating Funds");
            InstanceData Inst = StateFundingGlobal.fetch.GameInstance;
            if (Inst == null)
            {
                Log.Error("Review.UpdateFunds, GameInstance is null");
                return;
            }
            funds = (int)(((float)(finalPO + finalSC) / 10000 / 4) * (float)Inst.Gov.gdp * (float)Inst.Gov.budget);
        }

        public string GetText()
        {
            //InstanceData Inst = StateFundingGlobal.fetch.GameInstance;
            string returnText = "# Review for Quarter: " + year + "\n\n" +
                                "Funding: " + funds + "\n\n" +
                                "Public Opinion: " + po + "\n" +
                                "State Confidence: " + sc + "\n" +
                                "Public Opinion After Modifiers & Decay: " + finalPO + "\n" +
                                "State Confidence After Modifiers & Decay: " + finalSC + "\n\n" +
                                "Vessels Destroyed: " + vesselsDestroyed + "\n";
            foreach (Factor factor in factors)
            {
                returnText += factor.GetSummaryText();
            }
            return returnText;
        }
    }
}