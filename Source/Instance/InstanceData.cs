using System;

namespace StateFunding
{

    public class InstanceData
    {
        public Government Gov;

        [Persistent]
        public Review ActiveReview;

        [Persistent]
        public string govName;

        [Persistent]
        public int po;

        [Persistent]
        public int sc;

        [Persistent]
        public Review[] Reviews = new Review[0];

        public static InstanceData instance;

        public static InstanceData getInstance() {
            if (instance == null)
            {
                instance = new InstanceData();
            }
            return instance;
        }


        private InstanceData()
        {
            Log.Info("InstanceData");
            ActiveReview = new Review();
        }

        public void addReview(Review R)
        {
            Review[] NewReviews = new Review[Reviews.Length + 1];
            for (int i = 0; i < Reviews.Length; i++)
            {
                NewReviews[i] = Reviews[i];
            }
            NewReviews[NewReviews.Length - 1] = R;
            InstanceData Inst = StateFundingGlobal.fetch.GameInstance;
            Inst.po = R.finalPO;
            Inst.sc = R.finalSC;
            Reviews = NewReviews;
        }

        public Review[] getReviews()
        {
            return Reviews;
        }
    }
}

