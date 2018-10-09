﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core.Math
{
    public class DiscreteDistribution<T>
    {
        private readonly Dictionary<T, double> _densities;
        private bool _normalized;
        public DiscreteDistribution()
        {
            _normalized = false;
            _densities = new Dictionary<T, double>();
        }

        public double this[T item]
        {
            get
            {
                return _densities[item];
            }
            set
            {
                _densities[item] = value;
                _normalized = false;
            }
        }

        public void Add(T item, double probability)
        {
            _normalized = false;
            _densities[item] = probability;
        }

        private void Normalize()
        {
            var sum = _densities.Select(kv => kv.Value).Sum();
            double last = 0;
            foreach (T t in _densities.Keys.ToArray())
            {
                _densities[t] /= sum;
                _densities[t] += last;
                last = _densities[t];
            }
        }

        public T Sample()
        {
            if (!_normalized)
            {
                Normalize();
                _normalized = true;
            }

            var sample = Sampling.GetUniform();
            double last = 0;
            foreach (T t in _densities.Keys)
            {
                if (sample > last && sample <= _densities[t])
                    return t;
                last = _densities[t];
            }

            throw new IndexOutOfRangeException("There was a problem");
        }

        public bool Contains(T dow)
        {
            return _densities.ContainsKey(dow);
        }
    }
}
