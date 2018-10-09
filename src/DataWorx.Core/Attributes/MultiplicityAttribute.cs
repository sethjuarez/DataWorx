using System;
using System.Linq;
using DataWorx.Core.SetBuilders;
using System.Collections.Generic;
using DataWorx.Core.ObjectBuilders;

namespace DataWorx.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public abstract class MultiplicityAttribute : Attribute
    {
        public abstract ISetBuilder GetSetBuilder(IObjectBuilder builder);
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CountAttribute : MultiplicityAttribute
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public CountAttribute(int max)
            : this(0, max)
        {
            
        }

        public CountAttribute(int min, int max)
        {
            Max = max;
            Min = min;
        }

        public override ISetBuilder GetSetBuilder(IObjectBuilder builder)
        {
            return new BetweenSet(builder, Min, Max);
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SetAttribute : MultiplicityAttribute
    {
        public string Target { get; private set; }
        public object[] Set { get; private set; }

        public SetAttribute(string target, params object[] args)
        {
            Set = args;
            Target = target;
        }

        public override ISetBuilder GetSetBuilder(IObjectBuilder builder)
        {
            return new FixedSet(builder, Target, Set);
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class StaticSetAttribute : MultiplicityAttribute
    {
        public int Count { get; private set; }

        public StaticSetAttribute(int count)
        {
            Count = count;
        }

        public override ISetBuilder GetSetBuilder(IObjectBuilder builder)
        {
            return new StaticSet(builder, Count);
        }
    }
}
