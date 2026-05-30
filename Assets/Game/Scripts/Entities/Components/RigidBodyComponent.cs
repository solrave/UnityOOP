using UnityEngine;

namespace Game.Gameplay
{
    public class RigidbodyComponent
    {
        private readonly Rigidbody2D _transform;

        public RigidbodyComponent(Rigidbody2D transform)
        {
            _transform = transform;
        }

        public Rigidbody2D Body => _transform;

        public Vector2 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        public Quaternion Rotation
        {
            get => _transform.transform.rotation;
            set => _transform.transform.rotation = value;
        }
        
        public int Layer
        {
            get => _transform.gameObject.layer;
            set => _transform.gameObject.layer = value;
        }
        
        public Vector3 Forward => _transform.transform.forward;
        public Vector3 Right => _transform.transform.right;
    }
}