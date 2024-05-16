using System;
using UnityEngine;
using ModularWeapons.HealthSystem;

namespace ModularWeapons.Bullet
{
    public abstract class BulletTemplate : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _tracer;

        [SerializeField] private float _damage;

        protected float Damage => _damage;

        public void AssignDamage(float damage)
        {
            if(damage < 0) throw new ArgumentOutOfRangeException(nameof(damage), "Value cannot be lower than zero!");
            _damage = damage;
        }

        public abstract void Fire(Vector3 origin, Vector3 forward, Vector2 spread);
    }
}
