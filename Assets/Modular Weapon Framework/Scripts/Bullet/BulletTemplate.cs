using System;
using UnityEngine;
using ModularWeapons.HealthSystem;

namespace ModularWeapons.Bullet
{
    public abstract class BulletTemplate : MonoBehaviour
    {
        public event Action<Health> OnHit;
        [SerializeField] private ParticleSystem _tracer;

        protected void InvokeHit(Health health)
        {
            if (health == null)
            {
                throw new ArgumentNullException(nameof(health));
            }

            OnHit?.Invoke(health);
        }

        public virtual void Fire(Vector3 origin, Vector3 forward, Vector2 spread) {}
    }
}
