using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core
{
    internal static class TypeExtensions
    {
        internal static bool HasEquivalentKey<K>(this Dictionary<Type, K> dictionary, Type t)
        {
            foreach (Type key in dictionary.Keys)
                if (key.IsAssignableFrom(t))
                    return true;
            return false;
        }

        internal static K GetEquivalentValue<K>(this Dictionary<Type, K> dictionary, Type t)
        {
            foreach (Type key in dictionary.Keys)
                if (key.IsAssignableFrom(t))
                    return dictionary[key];
            return default(K);
        }

        internal static bool HasEquivalentKey<K>(this Dictionary<Tuple<Type, string>, K> dictionary, Type t, string name)
        {
            foreach (Tuple<Type, string> key in dictionary.Keys)
                if (key.Item1.IsAssignableFrom(t) && key.Item2 == name)
                    return true;
            return false;
        }

        internal static K GetEquivalentValue<K>(this Dictionary<Tuple<Type, string>, K> dictionary, Type t, string name)
        {
            foreach (Tuple<Type, string> key in dictionary.Keys)
                if (key.Item1.IsAssignableFrom(t) && key.Item2 == name)
                    return dictionary[key];
            return default(K);
        }
    }
}
