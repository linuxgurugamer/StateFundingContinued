using StateFunding.Factors;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KSP.Localization;

namespace StateFunding
{
    public class StateFunding
    {
        public List<Government> Governments;


        public ReviewManager ReviewMgr
        {
            get
            {
                if (StateFundingScenario.Instance != null)
                    return StateFundingScenario.Instance.ReviewMgr;
                return null;
            }
        }
        public InstanceData GameInstance
        {
            get
            {
                if (StateFundingScenario.Instance != null)
                    return StateFundingScenario.Instance.Data;
                return null;
            }
        }

        private StateFundingApplicationLauncher AppLauncher;


        public StateFunding() { }


        private void InitGovernments()
        {
            Governments = new List<Government>();

            ConfigNode GovConfig = ConfigNode.Load(KSPUtil.ApplicationRootPath + "GameData/StateFunding/data/governments.settings");
            ConfigNode[] GovItems = GovConfig.GetNode("Governments").GetNodes();
            for (var i = 0; i < GovItems.Length; i++)
            {
                ConfigNode GovItem = GovItems[i];
                Government Gov = new Government();

                Gov.name = Localizer.Format(GovItem.GetValue("name"));
                Gov.longName = Localizer.Format(GovItem.GetValue("longName"));
                Gov.poModifier = float.Parse(GovItem.GetValue("poModifier"));
                Gov.poPenaltyModifier = float.Parse(GovItem.GetValue("poPenaltyModifier"));
                Gov.scModifier = float.Parse(GovItem.GetValue("scModifier"));
                Gov.scPenaltyModifier = float.Parse(GovItem.GetValue("scPenaltyModifier"));
                Gov.startingPO = int.Parse(GovItem.GetValue("startingPO"));
                Gov.startingSC = int.Parse(GovItem.GetValue("startingSC"));
                Gov.budget = float.Parse(GovItem.GetValue("budget"));
                Gov.budgetPeriodsPerYear = int.Parse(GovItem.GetValue("budgetPeriodsPerYear"));
                Gov.gdp = int.Parse(GovItem.GetValue("gdp"));
                if (Gov.budget > 1)
                    Gov.budget = Gov.budget / Gov.gdp;
                Gov.description = Localizer.Format(GovItem.GetValue("description"));

                Governments.Add(Gov);

                Log.Info("Loaded Government: " + GovItem.GetValue("name"));
            }

            Log.Info("Initialized Governments");
        }

        private void InitEvents()
        {
            Log.Info("InitEvents");
            GameEvents.onCrewKilled.Add(OnCrewKilled);
            GameEvents.OnCrewmemberLeftForDead.Add(OnCrewLeftForDead);
            GameEvents.onCrash.Add(OnCrash);
            GameEvents.Contract.onCompleted.Add(OnContractCompleted);
            GameEvents.Contract.onFailed.Add(OnContractFailed);
            //GameEvents.onCrashSplashdown.Add(OnCrashSplashdown);
        }
        internal void OnDestroy()
        {
            Log.Info("OnDestroy");
            GameEvents.onCrewKilled.Remove(OnCrewKilled);
            GameEvents.OnCrewmemberLeftForDead.Remove(OnCrewLeftForDead);
            GameEvents.onCrash.Remove(OnCrash);
            GameEvents.Contract.onCompleted.Remove(OnContractCompleted);
            GameEvents.Contract.onFailed.Add(OnContractFailed);
            //GameEvents.onCrashSplashdown.Remove(OnCrashSplashdown);
        }

        public void unload()
        {
            ViewManager.removeAll();
            AppLauncher.unload();
            OnDestroy();

            StateFundingGlobal.isLoaded = false;
        }

        public void load()
        {
            Log.Info("StateFunding Mod Loading");
            //AppLauncher = new StateFundingApplicationLauncher();
            AppLauncher = OnSpaceCentre.Instance.gameObject.AddComponent<StateFundingApplicationLauncher>();


            InitGovernments();
            InitEvents();
            VesselHelper.LoadAliases();
            StateFundingGlobal.isLoaded = true;

            //StateFundingGlobal.Sun = Planetarium.fetch.Sun.GetName();

            Log.Info("StateFunding Mod Loaded");

            if (StateFundingGlobal.needsDataInit)
            {
                Log.Info("StateFunding performing data init");
                var NewView = new NewInstanceConfigView();
                NewView.OnCreate((InstanceData Inst) =>
                {
                    for (int i = 0; i < StateFundingGlobal.fetch.Governments.ToArray().Length; i++)
                    {
                        Government Gov = StateFundingGlobal.fetch.Governments.ToArray()[i];
                        if (Gov.name == Inst.govName)
                        {
                            Inst.Gov = Gov;
                            break;
                        }
                    }
                    HighLogic.CurrentGame.Parameters.CustomParams<StateFundingSettings>().budgetPeriodsPerYear = Inst.Gov.budgetPeriodsPerYear;

                    StateFundingScenario.Instance.data = Inst;
                    StateFundingScenario.Instance.isInit = true;
                    StateFundingGlobal.needsDataInit = false;
                    Log.Info("StateFunding data init completed");
                    ReviewMgr.CompleteReview();
                });

            }
            else
            {
                for (int i = 0; i < StateFundingGlobal.fetch.Governments.ToArray().Length; i++)
                {
                    Government Gov = StateFundingGlobal.fetch.Governments.ToArray()[i];
                    if (Gov.name == StateFundingScenario.Instance.data.govName)
                    {
                        StateFundingScenario.Instance.data.Gov = Gov;
                    }
                }
            }
        }

        public void LoadIfNeeded()
        {
            if (!StateFundingGlobal.isLoaded)
            {
                load();
            }
        }

        public void loadSave()
        {
            /*
            if (GameInstance == null) {
              if ((GameInstance = InstanceConf.loadInstance ()) == null) {
                InstanceConf.createInstance ((Instance Inst) => {
                  GameInstance = Inst;
                  ReviewMgr.CompleteReview ();
                  InstanceConf.saveInstance (Inst);
                });
              }

              Log.Info ("StateFunding Save Loaded");
            }
            */
        }

        public void tick()
        {
            if (GameInstance != null)
            {
                if (GameInstance.getReviews().Length > 0)
                {
                    // int year = (int)(TimeHelper.Quarters(Planetarium.GetUniversalTime()));
                    int year = (int)(TimeHelper.Periods(Planetarium.GetUniversalTime(), HighLogic.CurrentGame.Parameters.CustomParams<StateFundingSettings>().budgetPeriodsPerYear));
                    if (year > ReviewMgr.LastReview().year)
                    {
                        Log.Info("Happy New Quarter!");
                        if ((HighLogic.CurrentGame.Parameters.CustomParams<StateFundingSettings>().stopWarpAtBudgetPeriod && TimeWarp.fetch != null) ||
                            (HighLogic.CurrentGame.Parameters.CustomParams<StateFundingSettings>().stopWarpOnNewYear && year % HighLogic.CurrentGame.Parameters.CustomParams<StateFundingSettings>().budgetPeriodsPerYear == 0))
                        {
                            TimeWarp.fetch.CancelAutoWarp();
                            TimeWarp.SetRate(0, false);
                        }

                        ReviewMgr.CompleteReview();
                    }
                }
            }
        }

        // Events

        public void OnCrewKilled(EventReport Evt)
        {
            Log.Warning("CREW KILLED");
            GameInstance.ActiveReview.variables.kerbalDeaths++;
        }

        public void OnCrewLeftForDead(ProtoCrewMember Crew, int id)
        {
            Log.Warning("CREW KILLED");
            GameInstance.ActiveReview.variables.kerbalDeaths++;
        }

        #region  NO_LOCALIZATION
        public void OnCrash(EventReport Evt)
        {
            Log.Info("OnCrash, part: " + Evt.origin.partInfo.title);
            if (VesselHelper.PartHasModuleAlias(Evt.origin, "Command") || VesselHelper.PartHasModuleAlias(Evt.origin, "AutonomousCommand"))
            {
                ModuleStateFundingDisposable m = null;
                if (Evt.origin.Modules.Contains("ModuleStateFundingDisposable"))
                    m = Evt.origin.Modules["ModuleStateFundingDisposable"] as ModuleStateFundingDisposable;
                if (m == null)
                    Log.Info("OnCrash, m is null");
                else
                    if (m.disposable)
                {
                    Log.Warning("EXPENDABLE VESSEL DESTROYED");
                }
                else
                {
                    Log.Warning("VESSEL DESTROYED");
                    GameInstance.ActiveReview.variables.vesselsDestroyed++;
                }
                //InstanceConf.saveInstance (GameInstance);
            }
        }

        void OnContractCompleted(Contracts.Contract contract)
        {
            Log.Warning("CONTRACT COMPLETED");
            GameInstance.ActiveReview.variables.contractsCompleted++;
        }

        void OnContractFailed(Contracts.Contract contract)
        {
            Log.Warning("CONTRACT FAILED");
            GameInstance.ActiveReview.variables.contractsFailed++;
        }


#if false
        public void OnCrashSplashdown(EventReport Evt)
        {
            Log.Info("OnCrashSplashdown, part: " + Evt.origin.partInfo.title);
            if (VesselHelper.PartHasModuleAlias(Evt.origin, "Command") || VesselHelper.PartHasModuleAlias(Evt.origin, "AutonomousCommand"))
            {
                Log.Warning("VESSEL DESTROYED");
                GameInstance.ActiveReview.vesselsDestroyed++;
                //InstanceConf.saveInstance (GameInstance);
            }
        }
#endif
    }
}

#endregion