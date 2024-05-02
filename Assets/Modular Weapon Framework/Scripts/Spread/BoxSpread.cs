using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Vector3[] points =
            {
                transform.position + transform.forward + new Vector3(-_horizontalSpread,-_verticalSpread),
                transform.position + transform.forward + new Vector3(-_horizontalSpread,_verticalSpread),
                transform.position + transform.forward + new Vector3(_horizontalSpread,_verticalSpread),
                transform.position + transform.forward + new Vector3(_horizontalSpread,-_verticalSpread),
                transform.position + transform.forward + new Vector3(-_horizontalSpread,-_verticalSpread)
            };
            Handles.DrawPolyLine(points);
        }
        #endif
    }
}
