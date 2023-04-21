using System;
using UnityEngine;

namespace Gameplay.Unit
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class UnitView : MonoBehaviour
    {
        public abstract UnitType UnitType { get; }

        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public Transform ThrowPoint { get; private set; }

        public event Action<float> DamageTaken = _ => { };

        private void OnValidate()
        {
            Rigidbody ??= GetComponent<Rigidbody>();

            if (Rigidbody != null)
            {
                Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
        }

        public void TakeDamage(float damage)
        {
            DamageTaken(damage);
        }
    }
}