using DataWorx.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.Text
{
    public class NameText : IText
    {
        private readonly DiscreteDistribution<string> _firstLetters;
        private readonly DiscreteDistribution<string> _letters;
        private readonly char[] _vowels;
        public NameText()
        {
            // init first letter distribution
            _firstLetters = new DiscreteDistribution<string>();
            _firstLetters.Add("a", 0.11602);
            _firstLetters.Add("b", 0.04702);
            _firstLetters.Add("c", 0.03511);
            _firstLetters.Add("d", 0.0267);
            _firstLetters.Add("e", 0.02007);
            _firstLetters.Add("f", 0.03779);
            _firstLetters.Add("g", 0.0195);
            _firstLetters.Add("h", 0.07232);
            _firstLetters.Add("i", 0.06286);
            _firstLetters.Add("j", 0.00597);
            _firstLetters.Add("k", 0.0059);
            _firstLetters.Add("l", 0.02705);
            _firstLetters.Add("m", 0.04374);
            _firstLetters.Add("n", 0.02365);
            _firstLetters.Add("o", 0.06264);
            _firstLetters.Add("p", 0.02545);
            _firstLetters.Add("q", 0.00173);
            _firstLetters.Add("r", 0.01653);
            _firstLetters.Add("s", 0.07755);
            _firstLetters.Add("t", 0.16671);
            _firstLetters.Add("u", 0.01487);
            _firstLetters.Add("v", 0.00649);
            _firstLetters.Add("w", 0.06753);
            _firstLetters.Add("x", 0.00037);
            _firstLetters.Add("y", 0.0162);
            _firstLetters.Add("z", 0.00034);


            // init letter distribution
            _letters = new DiscreteDistribution<string>();
            _letters.Add("a", 0.08167);
            _letters.Add("b", 0.01492);
            _letters.Add("c", 0.02782);
            _letters.Add("d", 0.04253);
            _letters.Add("e", 0.12702);
            _letters.Add("f", 0.02228);
            _letters.Add("g", 0.02015);
            _letters.Add("h", 0.06094);
            _letters.Add("i", 0.06966);
            _letters.Add("j", 0.00153);
            _letters.Add("k", 0.00772);
            _letters.Add("l", 0.04025);
            _letters.Add("m", 0.02406);
            _letters.Add("n", 0.06749);
            _letters.Add("o", 0.07507);
            _letters.Add("p", 0.01929);
            _letters.Add("q", 0.00095);
            _letters.Add("r", 0.05987);
            _letters.Add("s", 0.06327);
            _letters.Add("t", 0.09056);
            _letters.Add("u", 0.02758);
            _letters.Add("v", 0.00978);
            _letters.Add("w", 0.0236);
            _letters.Add("x", 0.0015);
            _letters.Add("y", 0.01974);
            _letters.Add("z", 0.00074);

            // vowels
            _vowels = new[] { 'a', 'e', 'i', 'o', 'u' };
        }

        public string Create(int min, int max)
        {
            return Create(Sampling.GetUniform(min, max));
        }

        public string Create(int length)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_firstLetters.Sample());
            int consonantTracker = 0;
            while (sb.Length < length)
            {
                // consonant?
                if (!_vowels.Contains(sb[sb.Length - 1]))
                {
                    consonantTracker++;
                    if (consonantTracker >= 2)
                    {
                        int idx = Sampling.GetUniform(4);
                        sb.Append(_vowels[idx]);
                        consonantTracker = 0;
                    }
                }

                // don't want to repeat letters
                string sample = _letters.Sample();
                while (sample[0] == sb[sb.Length - 1])
                    sample = _letters.Sample();

                sb.Append(sample);
            }

            // capitalize first
            char[] a = sb.ToString().ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}
