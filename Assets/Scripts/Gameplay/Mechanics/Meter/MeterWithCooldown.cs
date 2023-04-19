using System;
using Gameplay.Mechanics.Timer;

namespace Gameplay.Mechanics.Meter
{
    public sealed class MeterWithCooldown : IDisposable
    {
        public float FillPercentage => _fill / _maxFill;
        public bool IsOnCooldown => FillPercentage == 1.0f;

        public event Action OnCooldownStart = () => { };
        public event Action OnCooldownEnd = () => { };

        private readonly Timer.Timer _cooldownTimer;
        
        private float _maxFill;
        private float _fill;

        public MeterWithCooldown(float initialFillValue, float maxFillValue, float cooldown, TimerFactory timerFactory)
        {
            if (maxFillValue == 0.0f) throw new ArgumentException("Meter maximum cannot be initialized with zero!", nameof(maxFillValue));
            _fill = initialFillValue;
            _maxFill = maxFillValue;

            _cooldownTimer = timerFactory.Create(cooldown);
            _cooldownTimer.OnStart += CooldownStarted;
            _cooldownTimer.OnExpire += CooldownFinished;
        }

        public void Dispose()
        {
            _cooldownTimer.OnStart -= CooldownStarted;
            _cooldownTimer.OnExpire -= CooldownFinished;
            _cooldownTimer.Dispose();
        }
        
        public void Fill(float value)
        {
            if (_fill + value >= _maxFill)
            {
                _fill = _maxFill;
                _cooldownTimer.Start();
                return;
            }

            _fill += value;
        }
        
        public void SetMaxValue(float newMaxValue)
        {
            if (newMaxValue <= 0.0f) throw new ArgumentException("Meter maximum cannot be initialized with zero!", nameof(newMaxValue));
            
            float newCurrentValue = newMaxValue * FillPercentage;
            _fill = newMaxValue;
            _maxFill = newCurrentValue;
        }

        private void CooldownStarted()
        {
            OnCooldownStart.Invoke();
        }

        private void CooldownFinished()
        {
            OnCooldownEnd.Invoke();
            ResetFilling();
        }

        private void ResetFilling()
        {
            _fill = 0.0f;
        }
    }
}