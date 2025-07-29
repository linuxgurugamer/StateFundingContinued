using KSP.Localization;
using StateFunding.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateFunding.Factors
{
    class ContractsFactor : Factor
    {
        public override string FactorName() { return "ContractsFactor"; } // NO_LOCALIZATION
        public override int modSC => variables.modSCContracts;

        public ContractsFactor(FactorVariables factorVariables) : base(factorVariables)
        {
        }

        public override  void Update()
        {
            variables.modSCContracts = (int)(5 * variables.contractsCompleted * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
            variables.modSCContracts -= (int)(5 * variables.contractsFailed * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
        }

        public override List<ViewSummaryRow> GetSummaryRow()
        {
            return new List<ViewSummaryRow>()
            {
                new ViewSummaryRow(Localizer.Format("#LOC_StateFunding_Govt_Contracts_Completed") + (int)variables.contractsCompleted, 0, (int)(5 * variables.contractsCompleted * StateFundingGlobal.fetch.GameInstance.Gov.scModifier)),
                new ViewSummaryRow(Localizer.Format("#LOC_StateFunding_Govt_Contracts_Failed") + (int)variables.contractsFailed, 0, (int)(-5 * variables.contractsFailed * StateFundingGlobal.fetch.GameInstance.Gov.scModifier))
            };
        }
    }
}
