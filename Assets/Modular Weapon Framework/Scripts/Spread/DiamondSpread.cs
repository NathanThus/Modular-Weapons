using UnityEngine;

namespace ModularWeapons.Spread
{
    public class DiamondSpread : BulletSpread
    {
        [SerializeField] private float _verticalSpread;
        [SerializeField] private float _horizontalSpread;

        public override Vector2 GetSpread()
        {
            float x = Random.Range(0,_horizontalSpread);
            float y = Random.Range(0, _verticalSpread - ((_verticalSpread - x) / 100  * _verticalSpread));
            return new Vector2(x,y);
        }
    }
}
