using System;
using System.Linq;
using Xunit;
using DataWorx.Core.Math;
using DataWorx.Core.Text;
using System.Collections.Generic;

namespace DataWorx.Core.Tests
{
    [Trait("Category", "General")]
    public class TextTests
    {
        [Fact]
        public void Generate_Text()
        {
            LongText g = new LongText();
            g.Load();
            string text = "";
            for (int i = 0; i < 100; i++)
            {
                var size = Sampling.GetUniform(140, 1000);
                text = g.Create(size);
                Console.WriteLine(String.Format("{0} ({1})\n", text, text.Length));
                Assert.True(text.Length <= size);
            }
        }

        [Fact]
        public void Generate_Starts_With_Text()
        {
            LongText g = new LongText();
            g.Load();
            string text = "";
            for (int i = 0; i < 100; i++)
            {
                var size = Sampling.GetUniform(140, 1000);
                text = g.Create(size, "super beggining of test"); 
                Console.WriteLine(String.Format("{0} ({1})\n", text, text.Length));
                Assert.True(text.Length <= size);
            }
        }

        [Fact]
        public void Generate_Name()
        {
            Sampling.SetSeedFromSystemTime();
            NameText text = new NameText();
            for(int i = 0; i < 50; i++)
                Console.WriteLine(text.Create(4, 5));
        }
    }
}
