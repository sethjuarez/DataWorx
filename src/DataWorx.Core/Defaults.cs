using System;
using System.Linq;
using System.Collections.Generic;

namespace DataWorx.Core
{
    public static class Defaults
    {
        internal static Tuple<double, double> MinMaxNumber { get; set; }
        internal static Tuple<DateTime, DateTime> MinMaxDate { get; set; }
        internal static int StringLength { get; set; }
        internal static int ItemCount { get; set; }

        static Defaults()
        {
            MinMaxNumber = new Tuple<double, double>(0, 1000);
            MinMaxDate = new Tuple<DateTime, DateTime>(DateTime.Now.AddYears(-1), DateTime.Now);
            StringLength = 100;
            ItemCount = 20;
        }

        public static void SetStringLength(int length)
        {
            StringLength = length;
        }

        public static void SetNumericRange(double min, double max)
        {
            MinMaxNumber = new Tuple<double, double>(min, max);
        }

        public static void SetDateTimeRange(DateTime from, DateTime to)
        {
            MinMaxDate = new Tuple<DateTime, DateTime>(from, to);
        }

        public static void SetItemCount(int count)
        {
            ItemCount = count;
        }
    }
}
