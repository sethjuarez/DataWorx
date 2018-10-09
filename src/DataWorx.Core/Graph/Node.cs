using System;
using System.Linq;
using DataWorx.Core.SetBuilders;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataWorx.Core.Graph
{
    public class Node
    {
        public Node()
        {
            Type = null;
            Many = new List<Property>();
            One = new List<Property>();
            Properties = new List<FieldProperty>();
            Instances = new List<object>();
        }

        /// <summary>
        /// Node name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Builds set of items
        /// </summary>
        [TypeConverter(typeof(TypeConverter<ISetBuilder>))]
        public ISetBuilder SetBuilder { get; set; }

        /// <summary>
        /// Item type
        /// </summary>
        [Browsable(false)]
        public Type Type { get; set; }

        /// <summary>
        /// Depended relationships
        /// </summary>
        [Browsable(false)]
        public List<Property> Many { get; set; }

        /// <summary>
        /// Dependencies
        /// </summary>
        [Browsable(false)]
        public List<Property> One { get; set; }

        /// <summary>
        /// Properties
        /// </summary>
        public List<FieldProperty> Properties { get; set; }

        /// <summary>
        /// Pointer to graph
        /// </summary>
        [Browsable(false)]
        public ObjectGraph Graph { get; set; }

        /// <summary>
        /// Generated instances
        /// </summary>
        [Browsable(false)]
        public List<object> Instances { get; set; }

        public void GenerateSet()
        {
            Instances = SetBuilder.GetSet().ToList();
        }

        public T Generate<T>()
        {
            object o = SetBuilder.Builder.Generate();
            Instances.Add(o);
            return (T)o;
        }

        public void Persist()
        {
            if (Saveables.Contains(Type))
            {
                foreach (object item in Instances)
                    Saveables.Save(item);
            } // check for emtpy save method (just in case)
            else
            {
                var method = Type.GetMethod("Save");
                if (method != null)
                    foreach (object item in Instances)
                        method.Invoke(item, new object[] { });
            }
        }

        public override string ToString()
        {
            return Type == null ? "[NULL]" : Type.Name;
        }
    }
}
