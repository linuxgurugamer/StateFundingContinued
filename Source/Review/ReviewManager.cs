using System;
using UnityEngine;
using System.Collections;

namespace StateFunding
{
    public class ReviewManager
    {

        public void CompleteReview()
        {
            InstanceData Inst = StateFundingGlobal.fetch.GameInstance;
            if (Inst == null)
            {
                Log.Error("ReviewManager.CompleteReview, Inst is null");
                return;
            }
            Review Rev = Inst.ActiveReview;
            Rev.touch();

            // Closed for business
            Rev.pastReview = true;

            // Move review to past review
            Inst.addReview(Rev);

            // Start a new review
            Inst.ActiveReview = new Review();

            // Apply PO/SC decay on instance
            ApplyDecay();

            // Apply funds from Review
            Log.Info("Adding Funds: " + Rev.funds);
            Funding.Instance.AddFunds(Rev.funds, TransactionReasons.None);

            // Notify player that a review is available
            ReviewToastView Toast = new ReviewToastView(Rev);

            Log.Info("Generated Review");
        }

        public void ApplyDecay()
        {
            Log.Info("Applying Decay");
            InstanceData Inst = StateFundingGlobal.fetch.GameInstance;
            if (Inst == null)
            {
                Log.Error("ReviewManager.ApplyDecay, Inst is null");
                return;
            }

            if (Inst.po > 0)
            {
                int newPO = Inst.po - (int)Math.Ceiling(Inst.po * 0.2);
                newPO = Math.Max(0, newPO);

                Inst.po = newPO;
            }
            else
            {
                int newPO = Inst.po += (int)Math.Ceiling(Inst.po * -0.2);
                newPO = Math.Min(0, newPO);

                Inst.po = newPO;
            }

            if (Inst.sc > 0)
            {
                int newSC = Inst.sc - (int)Math.Ceiling(Inst.sc * 0.2);
                newSC = Math.Max(0, newSC);

                Inst.sc = newSC;
            }
            else
            {
                int newSC = Inst.sc += (int)Math.Ceiling(Inst.sc * -0.2);
                newSC = Math.Min(0, newSC);

                Inst.sc = newSC;
            }
        }

        public void OpenReview(Review Rev)
        {
            Log.Info("Viewing Review");
            ReviewView RevView = new ReviewView(Rev);
        }

        public Review LastReview()
        {
            InstanceData Inst = StateFundingGlobal.fetch.GameInstance;
            if (Inst == null)
            {
                Log.Error("ReviewManager.LastReview, Inst is null");
                return null;
            }
            Review[] Reviews = Inst.getReviews();
            return Reviews[Reviews.Length - 1];
        }

    }
}

