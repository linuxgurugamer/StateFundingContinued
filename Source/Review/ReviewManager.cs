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
            Rev.closeReview();

            // Move review to past review
            Inst.addReview(Rev);

            // Start a new review
            Inst.ActiveReview = new Review();

            // Apply funds from Review
            Log.Info("Adding Funds: " + Rev.funds);
            Funding.Instance.AddFunds(Rev.funds, TransactionReasons.None);

            // Notify player that a review is available
            ReviewToastView Toast = new ReviewToastView(Rev);

            Log.Info("Generated Review");
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

