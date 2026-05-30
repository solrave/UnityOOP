using UnityEngine;

namespace Game.Gameplay
{
    public class Point : MonoBehaviour, IPoint
    {
        public Vector2 Position => this.transform.position;
    }
}