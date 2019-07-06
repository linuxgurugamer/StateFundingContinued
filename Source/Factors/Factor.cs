
using StateFunding.Factors.Views;
using System.Collections.Generic;

namespace StateFunding.Factors
{
    //meh, better name?
    /**
     * anything that can increase or decrease public opinion or state confidence
     **/
    public abstract class Factor
    {
        public virtual int modPO => 0;
        public virtual int modSC => 0;
        public virtual IFactorView View => null;
        public Factor(Dictionary<string, double> factorVariables) { }
        public virtual void cleanup() { }
        public virtual void Update(Dictionary<string, double> factorVariables) { }
        public virtual string GetSummaryText(Dictionary<string, double> factorVariables) { return ""; }
    }
}
