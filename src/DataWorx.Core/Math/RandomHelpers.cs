using DataWorx.Core.Distribution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.Math
{
    public static class RandomHelpers
    {
        public static void AddOrUpdate<K, V>(this Dictionary<K, List<V>> dictionary, K key, V value)
        {
            if (dictionary.ContainsKey(key))
            {
                if (!dictionary[key].Contains(value))
                    dictionary[key].Add(value);
            }
            else
                dictionary[key] = new List<V> { value };
        }

        public static void AddOrUpdate<K, V>(this Dictionary<K, Dictionary<V, int>> dictionary, K key, V value)
        {
            if (dictionary.ContainsKey(key))
            {
                if (dictionary[key].ContainsKey(value))
                    dictionary[key][value] += 1;
                else
                    dictionary[key][value] = 1;
            }
            else
            {
                dictionary[key] = new Dictionary<V, int>();
                dictionary[key].Add(value, 1);
            }
        }

        public static void AddOrUpdate<K, V>(this Dictionary<K, V> dictionary, K key, Func<V, V> value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value(dictionary[key]);
            else
                dictionary[key] = value(default(V));
        }

        public static T GetRandom<T>(this IEnumerable<T> items)
        {
            int count = items.Count() - 1; // for 0 index start
            int element = Sampling.GetUniform(count);
            return items.ElementAt(element);
        }

        public static T GetRandom<T>(this IEnumerable<T> set, IDraw distribution)
        {
            var idx = distribution.Draw(set.Count());
            return set.ElementAt(idx);
        }

        public static T Draw<T>(this IDraw distribution, IEnumerable<object> set, int count = -1)
        {
            var c = count == -1 ? set.Count() : System.Math.Min(set.Count(), count);
            return (T)set.ElementAt(distribution.Draw(c));
        }
    }
}
