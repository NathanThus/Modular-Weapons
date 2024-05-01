using ModularWeapons.HealthSystem;
using UnityEditor;
using UnityEngine;

namespace ModularWeapons.Bullet
{
    public class HitScanBullet : BulletTemplate
    {
        [SerializeField] private float _range;
        [SerializeField] private LayerMask _mask;

        public override void Fire(Vector3 origin, Vector3 forward, Vector2 spread)
        {
            Ray ray = new(origin, new Vector3(forward.x + spread.x, forward.y + spread.y, forward.z));

            #if UNITY_EDITOR
            Debug.DrawRay(ray.origin,ray.direction * _range, Color.red, 3f);
            #endif
            
            if(Physics.Raycast(ray,out RaycastHit hitInfo,_range,_mask))
            {
                if(hitInfo.transform.TryGetComponent<Health>(out var health))
                {
                    health.Hit(Damage);
                }
            }
        }

        #if UNITY_EDITOR

         private void OnDrawGizmos()
        {
            Handles.color = Color.green;
            Handles.DrawLine(transform.position, transform.position + transform.forward * _range, 1f);    
        }

        #endif
    }
}
