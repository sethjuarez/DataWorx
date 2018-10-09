using System;
using System.Linq;
using System.Collections.Generic;

namespace DataWorx.Core
{
    public static class Creatables
    {
        private readonly static Dictionary<Tuple<Type, string>, Func<object>> _createables;
        static Creatables()
        {
            _createables = new Dictionary<Tuple<Type, string>, Func<object>>();
        }

        public static bool Contains<T>(string name = "")
        {
            return Contains(typeof(T));
        }

        public static T Get<T>(string name = "")
        {
            Type t = typeof(T);
            var o = Get(t);
            return (T)o;
        }

        public static void Add<T>(Func<T> create, string name = "")
        {
            Func<object> f = () => (object)create();
            var tuple = new Tuple<Type, string>(typeof(T), name);
            _createables.Add(tuple, f);
        }

        internal static bool Contains(Type t, string name = "")
        {
            return _createables.HasEquivalentKey(t, name);
        }

        internal static object Get(Type t, string name = "")
        {
            var exp = _createables.GetEquivalentValue(t, name);
            if (exp != null)
                return exp();
            else
                return null;
        }
    }
}