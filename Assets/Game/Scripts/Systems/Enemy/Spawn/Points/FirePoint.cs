using UnityEngine;

namespace Game
{
    public class FirePoint : MonoBehaviour, IPoint
    {
        public Vector3 Position => this.transform.position;
    }
}