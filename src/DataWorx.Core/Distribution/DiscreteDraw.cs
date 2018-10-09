using System;
using System.Linq;
using DataWorx.Core.Math;
using System.Collections.Generic;

namespace DataWorx.Core.Distribution
{
    public class DiscreteDraw : IDraw
    {
        private readonly List<double> _densities;
        private bool _normalized;
        public DiscreteDraw()
        {
            _normalized = false;
            _densities = new List<double>();
        }

        public DiscreteDraw(params double[] args)
        {
            _normalized = false;
            _densities = new List<double>();
            AddRange(args);
        }

        public void Add(double probability)
        {
            _normalized = false;
            _densities.Add(probability);
        }

        public void AddRange(params double[] args)
        {
            _normalized = false;
            _densities.AddRange(args);
        }

        public void Clear()
        {
            _densities.Clear();
        }

        private void Normalize()
        {
            var sum = _densities.Sum();
            double last = 0;
            for (int i = 0; i < _densities.Count; i++)
            {
                _densities[i] /= sum;
                _densities[i] += last;
                last = _densities[i];
            }
        }

        public int Draw(int setcount)
        {
            if (_densities.Count == 0) throw new IndexOutOfRangeException("No defined densities");
            if (setcount == 0) throw new IndexOutOfRangeException("Cannot draw from an empty set");

            if (!_normalized)
            {
                Normalize();
                _normalized = true;
            }

            // get index into distribution
            var sample = Sampling.GetUniform();
            double last = 0;
            int index = -1;
            for (int i = 0; i < _densities.Count; i++)
            {
                if (sample > last && sample <= _densities[i])
                {
                    index = i;
                    break;
                }
                last = _densities[i];
            }

            if (index > -1 && index < setcount)
                return index;
            else
                throw new InvalidOperationException("Bad index...");
        }
    }
}
