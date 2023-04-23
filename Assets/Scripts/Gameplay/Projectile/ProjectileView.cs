using Gameplay.Unit;
using System;
using UnityEngine;

namespace Gameplay.Projectile
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(AudioClip))]
    public sealed class ProjectileView : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
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

            _audioSource ??= GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audioSource.Play();
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
