using UnityEngine;

namespace Game.Gameplay
{
    public class SpawnPoint : MonoBehaviour, IPoint
    {
        public Vector2 Position => this.transform.position;
    }
}