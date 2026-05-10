using UnityEngine;

namespace Game
{
    public class SpawnPoint : MonoBehaviour, IPoint
    {
        public Vector3 Position => this.transform.position;
    }
}