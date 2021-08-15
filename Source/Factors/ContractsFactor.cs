using StateFunding.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateFunding.Factors
{
    class ContractsFactor : Factor
    {
        public override string FactorName() { return "ContractsFactor"; }
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
                new ViewSummaryRow("Govt. Contracts Completed: " + (int)variables.contractsCompleted, 0, (int)(5 * variables.contractsCompleted * StateFundingGlobal.fetch.GameInstance.Gov.scModifier)),
                new ViewSummaryRow("Govt. Contracts Failed: " + (int)variables.contractsFailed, 0, (int)(-5 * variables.contractsFailed * StateFundingGlobal.fetch.GameInstance.Gov.scModifier))
            };
        }
    }
}
