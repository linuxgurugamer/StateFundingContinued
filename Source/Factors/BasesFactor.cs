using StateFunding.Factors.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateFunding.Factors
{
    public class BasesFactor : Factor
    {
        public override int modPO => variables.Bases.Sum(x => x.po);
        public override int modSC => variables.Bases.Sum(x => x.sc);
        public override IFactorView View => new StateFundingHubBasesView();
        
        public BasesFactor(FactorVariables _variables) : base(_variables)
        {
            variables.Bases = new BaseReport[0];
        }

        public override void Update()
        {
            Log.Info("Updating Bases");

            InstanceData GameInstance = StateFundingGlobal.fetch.GameInstance;
            if (GameInstance == null)
            {
                Log.Error("Review.UpdateBases, GameInstance is null");
                return;
            }

            Vessel[] _Bases = VesselHelper.GetBases();
            variables.Bases = new BaseReport[_Bases.Length];

            for (int i = 0; i < _Bases.Length; i++)
            {
                Vessel Base = _Bases[i];
                BaseReport _BaseReport = new BaseReport();
                _BaseReport.name = Base.vesselName;
                _BaseReport.crew = VesselHelper.GetCrew(Base).Length;
                _BaseReport.crewCapacity = VesselHelper.GetCrewCapactiy(Base);
                _BaseReport.dockedVessels = VesselHelper.GetDockedVesselsCount(Base);
                _BaseReport.dockingPorts = VesselHelper.GetDockingPorts(Base).Length;
                _BaseReport.drill = VesselHelper.VesselHasModuleAlias(Base, "Drill");
                _BaseReport.scienceLab = VesselHelper.VesselHasModuleAlias(Base, "ScienceLab");
                _BaseReport.fuel = VesselHelper.GetResourceCount(Base, "LiquidFuel");
                _BaseReport.ore = VesselHelper.GetResourceCount(Base, "Ore");
                _BaseReport.entity = Base.mainBody.name;

                _BaseReport.po = 0;
                _BaseReport.sc = 0;

                _BaseReport.po += (int)(5 * _BaseReport.crew * GameInstance.Gov.poModifier);
                _BaseReport.po += (int)(5 * _BaseReport.dockedVessels * GameInstance.Gov.poModifier);
                _BaseReport.po += (int)((Base.mainBody.Radius / StateFundingGlobal.refRadius) * (_BaseReport.dockedVessels + 1) * GameInstance.Gov.poModifier);

                _BaseReport.sc += (int)(2 * _BaseReport.crewCapacity * GameInstance.Gov.scModifier);
                _BaseReport.sc += (int)(_BaseReport.fuel / 200f * GameInstance.Gov.scModifier);
                _BaseReport.sc += (int)(_BaseReport.ore / 200f * GameInstance.Gov.scModifier);
                _BaseReport.sc += (int)(2 * _BaseReport.dockingPorts * GameInstance.Gov.scModifier);
                _BaseReport.sc += (int)(2 * _BaseReport.crewCapacity * GameInstance.Gov.scModifier);

                if (_BaseReport.scienceLab)
                {
                    _BaseReport.po += (int)(10 * GameInstance.Gov.poModifier);
                    _BaseReport.sc += (int)(10 * GameInstance.Gov.scModifier);
                }

                if (_BaseReport.drill)
                {
                    _BaseReport.po += (int)(10 * GameInstance.Gov.poModifier);
                    _BaseReport.sc += (int)(10 * GameInstance.Gov.scModifier);
                }

                variables.Bases[i] = _BaseReport;
            }
        }

        public override string GetSummaryText()
        {
            string returnText = "";
            if ((variables.Bases != null) && (variables.Bases.Length > 0))
            {
                returnText += "\n\n== Bases ==\n\n";
                for (int i = 0; i < variables. Bases.Length; i++)
                {
                    BaseReport Base = variables.Bases[i];
                    returnText += "[" + Base.name + " Landed At " + Base.entity + "]\n";
                    returnText += "Fuel: " + Base.fuel + "\n";
                    returnText += "Ore: " + Base.ore + "\n";
                    returnText += "Crew: " + Base.crew + "\n";
                    returnText += "Crew Capacity: " + Base.crewCapacity + "\n";
                    returnText += "Docked Vessels: " + Base.dockedVessels + "\n";
                    returnText += "Docking Ports: " + Base.dockingPorts + "\n";
                    returnText += "Has Drill: " + Base.drill + "\n";
                    returnText += "Science Lab: " + Base.scienceLab + "\n";
                    returnText += "PO: " + Base.po + "\n";
                    returnText += "SC: " + Base.sc + "\n\n";
                }
            }
            return returnText;
        }
    }
}
