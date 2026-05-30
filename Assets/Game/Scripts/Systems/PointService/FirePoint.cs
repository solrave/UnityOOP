using UnityEngine;

namespace Game.Gameplay
{
    public class FirePoint : MonoBehaviour, IPoint
    {
        public Vector2 Position => this.transform.position;
    }
}