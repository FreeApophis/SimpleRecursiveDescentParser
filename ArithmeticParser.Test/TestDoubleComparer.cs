﻿using System.Collections.Generic;

namespace ArithmeticParser.Test
{
    class MagnitudeDoubleComparer : IEqualityComparer<double>
    {
        private readonly double _maxDelta;

        public MagnitudeDoubleComparer(double maxDelta)
        {
            _maxDelta = maxDelta;
        }
        public bool Equals(double x, double y)
        {
            return x - y < _maxDelta;
        }

        public int GetHashCode(double obj)
        {
            throw new System.NotImplementedException();
        }
    }
}