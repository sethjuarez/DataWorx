using System;
using System.Linq;
using Xunit;
using DataWorx.Core.Math;
using DataWorx.Core.Text;
using System.Collections.Generic;
using static System.Math;

namespace DataWorx.Core.Tests
{
    [Trait("Category", "General")]
    public class RandomTests
    {
        [Fact]
        public void Generate_Unseeded_Random()
        {
            Sampling.SetSeedFromSystemTime();
            var x = 0;
            while (++x < 20)
            {
                var r = Sampling.GetUniform();
                Console.WriteLine(r);
            }
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(25)]
        public void Generate_Random_Zero_Range(int n)
        {

        }

        [Theory]
        [InlineData(5, 10)]
        [InlineData(100, 200)]
        public void Generate_Random_Range(int n1, int n2)
        {

        }

        [Fact]
        public void Generate_Random_Weights()
        {

        }

        [Fact]
        public void zscore_test()
        {
            double delta = .005;
            double val = 0;

            decimal[] tries = new decimal[] { .001m, .009m, .1m, .4m, .5m, .8m, .543m, .021m, .943m };
            double[] values = new double[] 
            { 
                -3.0902323061678132,
                -2.3656181268642924,
                -1.2815515655446004,
                -0.25334710313579972,
                0,
                0.8416212335729143,
                0.1079945694091542,
                -2.0335201492530506,
                1.5804668183993615
            };

            for (int i = 0; i < tries.Length; i++)
            {
                val = (double)z.normsinv(tries[i]);
                Assert.True(Abs(values[i] - val) < delta);
            }
        }
    }
}
