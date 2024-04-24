using UnityEngine;

namespace ModularWeapons.Spread
{
    public class CircleSpread : BulletSpread
    {
        [SerializeField] private float _radius;
        public override Vector2 GetSpread()
        {
            float x = Random.Range(-_radius,_radius);
            float y = Mathf.Sqrt(Mathf.Pow(_radius, 2) - Mathf.Pow(x, 2)) - Mathf.Pow(0, 2) + Mathf.Pow(0, 2) - Random.Range(-_radius, _radius);
            return new Vector2(x,y);
        }
    }
}
