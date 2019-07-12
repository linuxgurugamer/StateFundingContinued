using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateFunding.Factors
{
    class ContractsFactor : Factor
    {
        public override int modSC => variables.modSCContracts;

        public ContractsFactor(FactorVariables factorVariables) : base(factorVariables)
        {
        }

        public override  void Update()
        {
            variables.modSCContracts = (int)(5 * variables.contractsCompleted * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
            variables.modSCContracts -= (int)(5 * variables.contractsFailed * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
        }

        public override string GetSummaryText()
        {
            return "Govt. Contracts Completed: " + (int)variables.contractsCompleted + "\n" +
                   "Govt. Contracts Failed: " + (int)variables.contractsFailed + "\n";
        }
    }
}
