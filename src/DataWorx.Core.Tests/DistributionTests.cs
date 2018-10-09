using DataWorx.Core.Distribution;
using DataWorx.Core.Math;
using DataWorx.Core.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.Tests
{
    [Trait("Category", "General")]
    public class DistributionTests
    {
        public enum Test
        {
            One,
            Two,
            Three,
            Four
        }

        [Fact]
        public void Test_Discrete_Distribution_Creation()
        {
            DiscreteDistribution<Test> distribution = new DiscreteDistribution<Test>();
            distribution.Add(Test.One, .1);
            distribution.Add(Test.Two, .2);
            distribution.Add(Test.Three, .3);
            distribution.Add(Test.Four, .4);

            Dictionary<Test, double> items = new Dictionary<Test,double>();
            for (int i = 0; i < 1000000; i++)
                items.AddOrUpdate(distribution.Sample(), d => d + 1);

            var sum = items.Select(kv => kv.Value).Sum();
            foreach (var t in items.Keys.ToArray())
                items[t] /= sum;

            Assert.Equal(.1, items[Test.One], 2);
            Assert.Equal(.2, items[Test.Two], 2);
            Assert.Equal(.3, items[Test.Three], 2);
            Assert.Equal(.4, items[Test.Four], 2);
        }

        [Theory, InlineData(10)]
        public void Test_Normal_Distribution_Creation(int length)
        {
            NormalDraw distribution = new NormalDraw();
            double[] draws = new double[length];
            Dictionary<int, double> items = new Dictionary<int, double>();
            for (int i = 0; i < 1000000; i++)
                draws[distribution.Draw(length)] += 1;

            var sum = draws.Sum();
            for (int i = 0; i < draws.Length; i++)
                draws[i] /= sum;
        }


        [Fact]
        public void Test_Name_Generation()
        {
            Sampling.SetSeedFromSystemTime();
            NameText text = new NameText();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(text.Create(4, 7));
            }
        }

        [Fact]
        public void Test_Date_Generation()
        {
            Sampling.SetSeedFromSystemTime();
            DateDraw distribution = new DateDraw();
            for (int i = 0; i < 100; i++)
                Console.WriteLine(distribution.Draw(DateTime.Now.AddYears(-1), DateTime.Now));
        }
    }
}
