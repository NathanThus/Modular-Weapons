using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularWeapons.Bullet
{
    public class ProjectileBullet : BulletTemplate
    {
        [SerializeField] private ProjectileBullet _bullet;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private float _speed = 100f;
        [SerializeField] private float _lifeTimeSeconds = 3f;

        public float Range { get { return 0f; } }

        public Rigidbody Rigidbody => _rigidBody;

        public override void Fire(Vector3 origin, Vector3 forward, Vector2 spread)
        {
            var bullet = Instantiate(_bullet, origin, Quaternion.identity, null);
            bullet.gameObject.SetActive(true);
            bullet.Rigidbody.AddForce(new Vector3(forward.x + spread.x, forward.y + spread.y, forward.z) * _speed, ForceMode.Impulse);
            Destroy(bullet.gameObject, _lifeTimeSeconds);
        }
    }
}
