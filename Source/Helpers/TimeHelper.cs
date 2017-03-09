using System;

namespace StateFunding
{
    public static class TimeHelper
    {
        static double daysInyear = 426.08f;
        static double monthsInYear = 66.23f;
        static double daysInMonth = daysInyear / monthsInYear;
        static double secsInMin = 60;
        static double minInHour = 60;
        static double secsInHour = secsInMin * minInHour;
        static double hoursInDay = 6;
        static double secsInDay = secsInHour * hoursInDay;
        static double secsInMonth = secsInDay * daysInMonth;
        static double secsInyear = daysInyear * secsInDay;

        static public void SetKSPStockCalendar()
        {
            hoursInDay = GameSettings.KERBIN_TIME ? 6f : 24f;
            daysInyear = GameSettings.KERBIN_TIME ? 426.08f : 365f;
            if (Planetarium.fetch != null)
            {
                secsInDay = Planetarium.fetch.Home.solarDayLength;

                if (GameSettings.KERBIN_TIME)
                    secsInyear = Planetarium.fetch.Home.orbit.period;
                Log.Info("secsInDay: " + secsInDay.ToString() + "   secsInyear: " + secsInyear.ToString());
                Log.Info("solarDayLength: " + Planetarium.fetch.Home.solarDayLength.ToString());
            }
            //
            // Recalculate those values here which can change if the secsInYear or secsInDay change
            //
            daysInMonth = daysInyear / monthsInYear;
            daysInyear = secsInyear / secsInDay;
            secsInHour = secsInMin * minInHour;

            secsInMonth = secsInDay * daysInMonth;
            secsInyear = daysInyear * secsInDay;



        }

#if false
        public static int Minutes(double s)
        {
            return (int)(s / secsInMin);
        }

        public static int Hours(double s)
        {
            return (int)(s / secsInHour);
        }
#endif
        public static int Days(double s)
        {
            return (int)(s / secsInDay);
        }

#if false
        public static int Months(double s)
        {
            return (int)(s / secsInMonth);
        }

        public static int Years(double s)
        {
            return (int)(s /secsInyear);
        }
#endif
        public static int Quarters(double s)
        {
            return (int)(s / secsInyear * 4);
        }
        public static int Periods(double s, int periodsPerYear)
        {
            return (int)(s / secsInyear * periodsPerYear);
        }
#if false
        public static int ToMinutes(int s)
        {
            return (int)(s * secsInMin);
        }

        public static int ToHours(int s)
        {
            return (int)(s * secsInHour);
        }

        public static int ToDays(int s)
        {
            return (int)(s *secsInDay);
        }

        public static int ToMonths(int s)
        {
            return (int)(s * secsInMonth);
        }
#endif
        public static int ToYears(int s)
        {
            return (int)(s * secsInyear);
        }
#if false
        public static int ToQuarters(int s)
        {
            return (int)(s * secsInyear / 4);
        }
#endif
    }
}

