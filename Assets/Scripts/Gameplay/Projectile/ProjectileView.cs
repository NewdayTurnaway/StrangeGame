using Gameplay.Unit;
using System;
using UnityEngine;

namespace Gameplay.Projectile
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public sealed class ProjectileView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        public event Action CollidedObject = () => { };
        public event Action<UnitView> DamagedUnit = _ => { };

        private void OnValidate()
        {
            Rigidbody ??= GetComponent<Rigidbody>();

            if (Rigidbody != null)
            {
                Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out UnitView unitView))
            {
                DamagedUnit.Invoke(unitView);
            }
            else
            {
                CollidedObject.Invoke();
            }
        }
    }
}
