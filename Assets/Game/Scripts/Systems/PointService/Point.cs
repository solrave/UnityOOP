using UnityEngine;

namespace Game
{
    public class Point : MonoBehaviour, IPoint
    {
        public Vector3 Position => this.transform.position;
    }
}