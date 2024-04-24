using System;
using UnityEngine;

namespace ModularWeapons.Health
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Health : MonoBehaviour
    {
        #region Events
        public event Action OnDeath;
        public event Action OnHeal;
        public event Action<float> OnDamage;

        #endregion

        #region Serialized Fields

        [Header("Health Bar Properties")]
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;

        #endregion

        #region Properties

        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;

        #endregion

        #region Public

        public virtual void Hit(float damage)
        {
            if(damage < 0) throw new ArgumentOutOfRangeException(nameof(damage));

            _currentHealth -= damage;

            if(_currentHealth <= 0)
            {
                OnDeath?.Invoke();
                return;
            }

            OnDamage?.Invoke(damage);
        }

        public virtual void Heal(float hitpoints)
        {
            if(hitpoints < 0) throw new ArgumentOutOfRangeException(nameof(hitpoints));

            if(_currentHealth + hitpoints > MaxHealth)
            {
                _currentHealth = MaxHealth;
            }
            else
            {
                _currentHealth += hitpoints;
            }

            OnHeal?.Invoke();
        }

        #endregion
    }
}
