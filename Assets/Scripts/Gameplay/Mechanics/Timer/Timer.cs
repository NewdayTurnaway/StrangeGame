using System;
using Services;

namespace Gameplay.Mechanics.Timer
{
    public sealed class Timer : IDisposable
    {
        private readonly Updater _updater;
        public event Action OnStart = () => { };
        public event Action OnTick = () => { };
        public event Action OnExpire = () => { };

        public bool InProgress => _currentValue > 0.0f;
        public bool IsExpired => _currentValue == 0.0f;
        public float CurrentValue => _currentValue;

        private float _maxValue;
        private float _currentValue;
        private bool _isPause;
        
        public Timer(float value, Updater updater)
        {
            if (value <= 0.0f) throw new ArgumentException("Timer cannot be initialized with zero!", nameof(value));
            
            _updater = updater;
            
            _maxValue = value;
            _currentValue = 0.0f;

            _updater.SubscribeToUpdate(Tick);
        }
        
        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(Tick);
        }

        public void Start()
        {
            _currentValue = _maxValue;
            OnStart.Invoke();
        }
        
        public void Pause()
        {
            _isPause = true;
        }

        public void Resume()
        {
            _isPause = false;
        }

        public void SetMaxValue(float newMaxValue)
        {
            if (newMaxValue == 0.0f) throw new ArgumentException("Timer cannot be initialized with zero!", nameof(newMaxValue));
            
            float currentPercentage = _currentValue / _maxValue;
            float newCurrentValue = newMaxValue * currentPercentage;
            _maxValue = newMaxValue;
            _currentValue = newCurrentValue;
        }

        public void SetToZero()
        {
            _currentValue = 0.0f;
            OnExpire.Invoke();
        }

        private void Tick(float deltaTime)
        {
            if(_isPause) return;

            switch (_currentValue)
            {
                case 0:
                    return;
                case < 0:
                    _currentValue = 0.0f;
                    OnExpire.Invoke();
                    return;
                case > 0:
                    _currentValue -= deltaTime;
                    OnTick.Invoke();
                    return;
            }
        }
    }
}