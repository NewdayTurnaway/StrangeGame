using System;

namespace Gameplay.Mechanics.Meter
{
    public sealed class Meter
    {
        public float FillPercentage => _fill / _maxFill;
        
        private readonly float _maxFill;
        private float _fill;

        public Meter(float initialFillValue, float maxFillValue)
        {
            if (maxFillValue == 0.0f) throw new ArgumentException("Meter maximum cannot be initialized with zero!", nameof(maxFillValue));
            _fill = initialFillValue;
            _maxFill = maxFillValue;
        }

        public void Fill(float value)
        {
            if (_fill + value >= _maxFill)
            {
                _fill = _maxFill;
                return;
            }

            _fill += value;
        }

        public bool TrySpend(float value)
        {
            if (_fill - value < 0.0f)
            {
                return false;
            }

            _fill -= value;
            return true;
        }

        public void SetToZero()
        {
            _fill = 0.0f;
        }
    }
}