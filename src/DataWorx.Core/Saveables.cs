using System;
using System.Linq;
using System.Collections.Generic;

namespace DataWorx.Core
{
    public static class Saveables
    {
        private readonly static Dictionary<Type, Action<object>> _saveables;
        static Saveables()
        {
            _saveables = new Dictionary<Type, Action<object>>();
        }

        public static bool Contains<T>()
        {
            return Contains(typeof(T));
        }

        public static bool Contains(Type t)
        {
            return _saveables.HasEquivalentKey(t);
        }

        public static void Save(object o)
        {
            Type t = o.GetType();
            var exp = _saveables.GetEquivalentValue(t);
            if (exp != null)
                exp(o);
        }

        public static void Save<T>(this T o)
        {
            Save(o);
        }

        public static void Add<T>(Action<T> save)
        {
            Action<object> f = o => save((T)o);
            _saveables.Add(typeof(T), f);
        }
    }
}
