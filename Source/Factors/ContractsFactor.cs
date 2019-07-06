using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateFunding.Factors
{
    class ContractsFactor : Factor
    {
        public override int modSC => _modSC;
        private int _modSC = 0;

        public static string contractsCompleted = "contractsCompleted";
        public static string contractsFailed = "contractsFailed";

        public ContractsFactor(Dictionary<string, double> factorVariables) : base(factorVariables)
        {
            factorVariables[contractsCompleted] = 0;
            factorVariables[contractsFailed] = 0;
        }

        public override  void Update(Dictionary<string, double> factorVariables)
        {
            _modSC = (int)(5 * factorVariables[contractsCompleted] * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
            _modSC -= (int)(5 * factorVariables[contractsFailed] * StateFundingGlobal.fetch.GameInstance.Gov.scModifier);
        }

        public override string GetSummaryText(Dictionary<string, double> factorVariables)
        {
            return "Govt. Contracts Completed: " + (int)factorVariables[contractsCompleted] + "\n" +
                   "Govt. Contracts Failed: " + (int)factorVariables[contractsFailed] + "\n";
        }
    }
}
