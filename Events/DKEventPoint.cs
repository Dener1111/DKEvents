using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKEvents
{
    public struct Point
    {
        public Vector3 position;
        public Quaternion rotation;
        
        public Point(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
        
        public static implicit operator Point(UnityEngine.Transform transform)
        {
            return new Point(transform.position, transform.rotation);
        }
    }

    [CreateAssetMenu(fileName = "DKEventPoint", menuName = "ScriptableObjects/DKEvents/Point", order = 9)]
    public class DKEventPoint : DKEventBase<Point> { }
}