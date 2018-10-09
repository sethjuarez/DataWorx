using DataWorx.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.FieldBuilders
{
    public class PriceFieldBuilder : IFieldBuilder
    {
        public double Drift { get; set; }
        public double Volatility { get; set; }
        public double Step { get; set; }
        public double Last { get; set; }

        public PriceFieldBuilder(double last = 100, double drift = .1, double volatility = .07, double step = .01)
        {
            Step = step;
            Last = last;
            Drift = drift;
            Volatility = volatility;
        }

        public object Create()
        {
            // geometric brownian motion
            var drift = Drift * Step * Last;
            var uncertainty = Volatility * System.Math.Sqrt(Step) * Last * GetUncertainty();

            Last = Last + drift + uncertainty;
            return (decimal)Last;        
        }

        private static double GetUncertainty()
        {
            return (double)z.normsinv((decimal)Sampling.GetUniform());
        }
    }
}
