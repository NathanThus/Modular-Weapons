using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularWeapons.Spread
{
    public class BoxSpread : BulletSpread
    {
        [SerializeField] private float _verticalSpread;
        [SerializeField] private float _horizontalSpread;
        
        public override Vector2 GetSpread()
        {
            return new Vector2(Random.Range(-_horizontalSpread, _horizontalSpread),
                               Random.Range(-_verticalSpread, _verticalSpread));
        }
    }
}
