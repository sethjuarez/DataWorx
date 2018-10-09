using DataWorx.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.Distribution
{
    public class DateDraw
    {
        private readonly DiscreteDistribution<DayOfWeek> _dayOfWeek;
        private readonly DiscreteDistribution<int> _months;
        private readonly DiscreteDistribution<int> _hours;

        public DateDraw()
        {
            _dayOfWeek = new DiscreteDistribution<DayOfWeek>();
            _months = new DiscreteDistribution<int>();
            _hours = new DiscreteDistribution<int>();
            InitDays();
            InitMonths();
            InitHours();
        }

        private void InitDays()
        {
            _dayOfWeek.Add(DayOfWeek.Sunday, 1);
            _dayOfWeek.Add(DayOfWeek.Monday, 1);
            _dayOfWeek.Add(DayOfWeek.Tuesday, 1);
            _dayOfWeek.Add(DayOfWeek.Wednesday, 1);
            _dayOfWeek.Add(DayOfWeek.Thursday, 1);
            _dayOfWeek.Add(DayOfWeek.Friday, 1);
            _dayOfWeek.Add(DayOfWeek.Saturday, 1);
        }

        private void InitMonths()
        {
            _months.Add(1, 1);
            _months.Add(2, 1);
            _months.Add(3, 1);
            _months.Add(4, 1);
            _months.Add(5, 1);
            _months.Add(6, 1);
            _months.Add(7, 1);
            _months.Add(8, 1);
            _months.Add(9, 1);
            _months.Add(10, 1);
            _months.Add(11, 1);
            _months.Add(12, 1);
        }

        private void InitHours()
        {
            _hours.Add(0, 1);
            _hours.Add(1, 1);
            _hours.Add(2, 1);
            _hours.Add(3, 1);
            _hours.Add(4, 1);
            _hours.Add(5, 1);
            _hours.Add(6, 1);
            _hours.Add(7, 1);
            _hours.Add(8, 1);
            _hours.Add(9, 1);
            _hours.Add(10, 1);
            _hours.Add(11, 1);
            _hours.Add(12, 1);
            _hours.Add(13, 1);
            _hours.Add(14, 1);
            _hours.Add(15, 1);
            _hours.Add(16, 1);
            _hours.Add(17, 1);
            _hours.Add(18, 1);
            _hours.Add(19, 1);
            _hours.Add(20, 1);
            _hours.Add(21, 1);
            _hours.Add(22, 1);
            _hours.Add(23, 1);
        }

        public void SetDayOfWeek(DayOfWeek dow, double d)
        {
            if (!_dayOfWeek.Contains(dow)) throw new IndexOutOfRangeException();
            _dayOfWeek[dow] = d;
        }

        public void SetDayOfWeek(params double[] densities)
        {
            for (int i = 0; i < densities.Length || i < 7; i++)
                SetDayOfWeek((DayOfWeek)i, densities[i]);
        }
        public void SetMonth(int month, double d)
        {
            if (!_months.Contains(month)) throw new IndexOutOfRangeException();
            _months[month] = d;
        }

        public void SetMonth(params double[] densities)
        {
            for (int i = 0; i < densities.Length || i < 12; i++)
                SetMonth(i + 1, densities[i]);
        }

        public void SetHour(int hour, double d)
        {
            if (!_hours.Contains(hour)) throw new IndexOutOfRangeException();
            _hours[hour] = d;
        }

        public void SetHour(params double[] densities)
        {
            for (int i = 0; i < densities.Length || i < 24; i++)
                SetHour(i, densities[i]);
        }

        public DateTime Draw(DateTime from, DateTime to)
        {
            var span = to.Subtract(from);
            if (span.TotalDays < 360) throw new InvalidOperationException("Can only work within year ranges for now...");

            var month = _months.Sample();
            var dow = _dayOfWeek.Sample();
            var hour = _hours.Sample();
            var day = DateTime.DaysInMonth(from.Year, month);
            var year = new DateTime(from.Year, month, System.Math.Min(from.Day, day)) < from ? from.Year + 1 : from.Year;

            var date = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                                 .Select(n => new DateTime(year, month, n))
                                 .Where(d => d.DayOfWeek == dow)
                                 .GetRandom()
                                 .AddHours(hour)
                                 .AddMinutes(Sampling.GetUniform(59))
                                 .AddSeconds(Sampling.GetUniform(59));

            return date;
        }
    }
}
