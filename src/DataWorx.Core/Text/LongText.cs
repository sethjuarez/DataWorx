using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataWorx.Core.Math;
using System.Reflection;
using DataWorx.Core.Distribution;

namespace DataWorx.Core.Text
{
    public class LongText : IText
    {
        public const string BOS = "BEGGINNING_OF_SENTENCE";
        public const string EOS = "END_OF_SENTENCE";
        private Dictionary<string, int> _words;
        public Dictionary<string, int> Words
        {
            get
            {
                return _words;
            }
        }

        public bool DiscreteDraw { get; set; }

        private Dictionary<Tuple<string, string>, SetDraw<string>> _dictionary;
        public LongText()
        {
            Sampling.SetSeedFromSystemTime();
            DiscreteDraw = true;
        }

        public void Load()
        {
            Assembly assembly = typeof(LongText).Assembly;
            var files = assembly.GetManifestResourceNames()
                                .Where(s => s.Contains("Text") && s.EndsWith("txt"))
                                .ToArray();
            Load(s => {
                    using (var stream = assembly.GetManifestResourceStream(s))
                    using (var reader = new StreamReader(stream))
                        return reader.ReadToEnd();
                },
                files
            );
        }

        public void Load(params string[] files)
        {
            Load(s => {
                    using (TextReader reader = new StreamReader(s))
                        return reader.ReadToEnd();
                },
                files
            );
        }

        public void Load(Func<string, string> generate, params string[] files)
        {
            _words = new Dictionary<string, int>();
            var dictionary = new Dictionary<Tuple<string, string>, Dictionary<string, int>>();
            var data = string.Empty;
            foreach (string file in files)
            {
                data = generate(file);

                var w = GetWords(data).ToArray();

                Tuple<string, string> key;
                for (int i = 0; i < w.Length - 2; i++)
                {
                    string item1 = w[i], item2 = w[i + 1], item3 = w[i + 2];

                    // markov list with 2-gram
                    key = new Tuple<string, string>(item1, item2);
                    dictionary.AddOrUpdate(key, item3);

                    // word frequency
                    _words.AddOrUpdate(item1, v => v + 1);
                }
            }

            // add statistic for drawing text
            _dictionary = new Dictionary<Tuple<string, string>, SetDraw<string>>();
            foreach (Tuple<string, string> key in dictionary.Keys)
            {
                if (!DiscreteDraw) // draw uniformly
                    _dictionary.Add(key, new SetDraw<string>(dictionary[key].Keys, new UniformDraw()));
                else
                {
                    Dictionary<string, int> set = dictionary[key];
                    var discrete = new DiscreteDraw();
                    discrete.AddRange(set.Values.Select(i=> (double)i).ToArray());
                    var drawFunction = new SetDraw<string>(set.Keys, discrete);
                    _dictionary.Add(key, drawFunction);
                }
            }

            dictionary.Clear();
            dictionary = null;
        }


        public Tuple<string, string> RandomTransition(string word1, string word2)
        {
            var key = new Tuple<string, string>(word1, word2);
            return RandomTransition(key);
        }

        public Tuple<string, string> RandomTransition(Tuple<string, string> key)
        {
            SetDraw<string> transition;
            if (_dictionary.ContainsKey(key))
                transition = _dictionary[key];
            else // uh oh, we went horribly wrong somewhere....
                transition = _dictionary.GetRandom().Value;

            return new Tuple<string, string>(key.Item2, transition.Draw());
        }

        private IEnumerable<string> GetWords(string data)
        {
            Func<char, bool> end = c => c == '.' || c == '?' || c == '!';
            Func<string, string> clean = v => v.Trim().Replace("\"", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace("_", "");
            var words = data.Split(' ', '\n', '\t', '\r', '-');
            bool start = true;
            for (int i = 0; i < words.Length; i++)
            {
                var w = clean(words[i]);
                if (!string.IsNullOrEmpty(w) && !string.IsNullOrWhiteSpace(w))
                {
                    if (char.IsPunctuation(w[w.Length - 1]))
                    {
                        string word = w.Substring(0, w.Length - 1);
                        yield return word;
                        // ends with punctuation (for start and end words)
                        if (end(w[w.Length - 1]) && !w.StartsWith("Mr"))
                        {
                            yield return EOS;
                            start = true;
                        }
                    }
                    else
                    {
                        if (start)
                        {
                            yield return BOS;
                            start = false;
                        }
                        yield return w;
                    }
                }
            }
        }

        public string Create(int min, int max)
        {
            return Create(Sampling.GetUniform(min, max) - 1);
        }

        public string Create(int length, string start)
        {
            return string.Empty;
        }

        public string Create(int length)
        {
            if (length == 0) return "";
            Func<string, string> capitalize = s => String.Format("{0}{1}", char.ToUpper(s[0]), s.Substring(1, s.Length - 1));
            var key = new Tuple<string, string>(EOS, BOS);
            List<string> generated = new List<string>();

            var query = from s in generated
                        where s != BOS && s != EOS
                        select s.Length + 1; // for the eventual space of course...

            // loop through transitions
            while (query.Sum() < length - 10) // for buffering
            {
                key = RandomTransition(key);
                generated.Add(key.Item2);
            }

            // trim off excess (if exists)
            var end = generated.LastIndexOf(EOS);
            if (end > 1)
                generated.RemoveRange(end, generated.Count - end);

            // apply propert capitalization
            for (int i = 0; i < generated.Count; i++)
            {
                if (i == 0)
                    generated[i] = capitalize(generated[i]);
                else if (generated[i - 1] == BOS)
                    generated[i] = capitalize(generated[i]);
            }

            // remove tokens
            var text = string.Join(" ", generated.ToArray());
            text = text.Replace(" " + EOS, ".").Replace(" " + BOS, "");

            // signal end of thought
            if (end < 0)
                text = text + "...";
            else
                text = text + ".";

            // trim to size
            if (text.Length > length)
                return text.Substring(0, length);
            else
                return text;
        }
    }
}
