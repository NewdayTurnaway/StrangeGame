using System;
using UnityEngine;
using Zenject;

namespace Services
{
    public sealed class Updater : ITickable, IFixedTickable, ILateTickable
    {
        private event Action OnUpdate = () => { };
        private event Action<float> OnDeltaTimeUpdate = _ => { };
        private event Action OnFixedUpdate = () => { };
        private event Action OnLateUpdate = () => { };
    
        public void SubscribeToUpdate(Action callback) => OnUpdate += callback;
        public void UnsubscribeFromUpdate(Action callback) => OnUpdate -= callback;
        public void SubscribeToUpdate(Action<float> callback) => OnDeltaTimeUpdate += callback;
        public void UnsubscribeFromUpdate(Action<float> callback) => OnDeltaTimeUpdate -= callback;
    
        public void SubscribeToFixedUpdate(Action callback) => OnFixedUpdate += callback;
        public void UnsubscribeFromFixedUpdate(Action callback) => OnFixedUpdate -= callback;    
    
        public void SubscribeToLateUpdate(Action callback) => OnLateUpdate += callback;
        public void UnsubscribeFromLateUpdate(Action callback) => OnLateUpdate -= callback;

        public void Tick()
        {
            OnUpdate.Invoke();
            OnDeltaTimeUpdate.Invoke(Time.deltaTime);
        }

        public void FixedTick()
        {
            OnFixedUpdate.Invoke();
        }

        public void LateTick()
        {
            OnLateUpdate.Invoke();
        }
    }
}