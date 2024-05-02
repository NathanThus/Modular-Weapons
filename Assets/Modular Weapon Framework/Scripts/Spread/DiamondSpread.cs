using UnityEditor;
using UnityEngine;

namespace ModularWeapons.Spread
{
    public class DiamondSpread : BulletSpread
    {
        [SerializeField] private float _verticalSpread;
        [SerializeField] private float _horizontalSpread;

        public override Vector2 GetSpread()
        {
            float x = Random.Range(-_horizontalSpread,_horizontalSpread);
            float verticalMax = _verticalSpread - ((_verticalSpread - x) / 100  * _verticalSpread);
            float y = Random.Range(-verticalMax, verticalMax);
            return new Vector2(x,y);
        }

        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Vector3[] points =
            {
                transform.position + transform.forward + new Vector3(-_horizontalSpread,0),
                transform.position + transform.forward + new Vector3(0,_verticalSpread),
                transform.position + transform.forward + new Vector3(_horizontalSpread,0),
                transform.position + transform.forward + new Vector3(0,-_verticalSpread),
                transform.position + transform.forward + new Vector3(-_horizontalSpread,0)
            };
            Handles.DrawPolyLine(points);
        }
        #endif
    }
}
