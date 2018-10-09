using System;
using System.Linq;
using System.Reflection;
using DataWorx.Core.Attributes;
using DataWorx.Core.SetBuilders;
using System.Collections.Generic;
using DataWorx.Core.FieldBuilders;
using DataWorx.Core.ObjectBuilders;

namespace DataWorx.Core.Graph
{
    public class ObjectGraph
    {
        private readonly Type[] _model;
        public ObjectGraph() { }

        public ObjectGraph(params Type[] model)
        {
            _model = model;
            Build(_model);
        }

        public List<Node> Nodes { get; set; }

        internal void Build(Type[] types)
        {
            Node[] nodes = new Node[types.Length];
            for (int i = 0; i < types.Length; i++)
            {
                Node node = new Node { Type = types[i], Name = types[i].Name };

                // object builder
                IObjectBuilder b = GetObjectBuilder(node);

                // set (multiplicity) builder
                node.SetBuilder = GetSetBuilder(node, b);

                // look at properties
                foreach (PropertyInfo pi in node.Type.GetProperties())
                {
                    Type returnType = pi.GetGetMethod().ReturnType;
                    // only interested in members declared locally
                    if (pi.DeclaringType == node.Type)
                    {
                        
                        // one side
                        if (types.Contains(returnType))
                            node.One.Add(Property.New(pi.Name, returnType));
                        // many side
                        else if (returnType.IsGenericType)
                        {
                            var args = pi.PropertyType.GetGenericArguments();
                            if (args.Length == 1 && types.Contains(args[0]))
                                node.Many.Add(Property.New(pi.Name, args[0]));
                        }
                        // regular field
                        else
                        {
                            // get appropriate field builder
                            var fieldBuilder = GetFieldBuilder(pi);
                            if (fieldBuilder != null)
                                node.Properties.Add(FieldProperty.New(pi.Name, returnType, fieldBuilder));
                        }
                    }
                }

                node.Graph = this;
                nodes[i] = node;
            }

            Sort(nodes);
        }

        private IObjectBuilder GetObjectBuilder(Node node)
        {
            // for constructing builders
            var mapping = node.Type.GetCustomAttributes<MapAttribute>();
            IObjectBuilder last = null;
            foreach (var m in mapping.Reverse())
            {
                var builder = m.CreateBuilder(node);
                builder.Next = last;
                last = builder;
            }
            var b = new ObjectBuilder(node) { Next = last };
            return b;
        }

        private ISetBuilder GetSetBuilder(Node node, IObjectBuilder b)
        {
            var set = node.Type.GetCustomAttribute<MultiplicityAttribute>();
            ISetBuilder generator = null;
            if (set != null) generator = set.GetSetBuilder(b);
            var sb = generator ?? new BetweenSet(b, (int)Defaults.MinMaxNumber.Item1, (int)Defaults.MinMaxNumber.Item2);
            return sb;
        }

        private IFieldBuilder GetFieldBuilder(PropertyInfo pi)
        {
            Type returnType = pi.GetGetMethod().ReturnType;

            // special string case with size attribute
            //if (returnType == typeof(string) && pi.GetCustomAttribute<SizeAttribute>() != null)
            //{
            //    var size = pi.GetCustomAttribute<SizeAttribute>();
            //    return new StringFieldBuilder(size.Size);
            //}


            var annotation = pi.GetCustomAttribute<FieldAttribute>();
            if (annotation != null)
                return annotation.CreateFieldBuilder(returnType);
            else
                return GetFieldBuilder(returnType);
        }

        private IFieldBuilder GetFieldBuilder(Type type)
        {
            TypeCode typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return new NumberFieldBuilder { NumericType = type, Min = Defaults.MinMaxNumber.Item1, Max = Defaults.MinMaxNumber.Item2 };
                case TypeCode.Boolean:
                    return new BoolFieldBuilder();
                case TypeCode.DateTime:
                    return new DateTimeFieldBuilder(Defaults.MinMaxDate.Item1, Defaults.MinMaxDate.Item2);
                case TypeCode.String:
                    return new StringFieldBuilder { Length = Defaults.StringLength };
                case TypeCode.Char:
                    //return new StringFieldBuilder(1);
                case TypeCode.Empty:
                case TypeCode.Object:
                case TypeCode.DBNull:
                    return null;
            }

            return null;
        }

        //private bool IsKey(PropertyInfo pi)
        //{
        //    // key? need to make some checks
        //    var key = pi.GetCustomAttribute<KeyAttribute>();
        //    if (key != null && !key.AutoGenerate)
        //        throw new NotSupportedException("Can only work with autogenerated keys at this time...");
        //    return key != null;
        //}
        
        private void Sort(Node[] nodes)
        {
            // sort by least amount of "in-edges"
            // this allows ordered item creation
            Nodes = new List<Node>(nodes.Length);
            int min = -1;
            do
            {
                min++;
                for (int i = 0; i < nodes.Length; i++)
                    if (nodes[i].One.Count == min)
                        Nodes.Add(nodes[i]);
            }
            while (Nodes.Count < nodes.Length);
        }

        public Node Find(Type type)
        {
            var node = Nodes.Where(n => n.Type == type).FirstOrDefault();
            if (node == null)
                throw new GraphException(string.Format("Node of type {0} does not exist!", type));
            return node;
        }

        public Node Find<T>()
        {
            return Find(typeof(T));
        }

        public void Generate()
        {
            foreach (Node node in Nodes)
                node.GenerateSet();
        }
    }
}