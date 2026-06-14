using UnityEngine;

namespace Game.Gameplay
{
    public class RigidbodyComponent
    {
        private readonly Rigidbody2D _body;

        public RigidbodyComponent(Rigidbody2D body)
        {
            _body = body;
        }

        public Rigidbody2D Body => _body;

        public Vector2 Position
        {
            get => _body.position;
            set => _body.position = value;
        }

        public Quaternion Rotation
        {
            get => _body.transform.rotation;
            set => _body.transform.rotation = value;
        }
        
        public int Layer
        {
            get => _body.gameObject.layer;
            set => _body.gameObject.layer = value;
        }
        
        public Vector3 Forward => _body.transform.forward;
        public Vector3 Right => _body.transform.right;

        public void MovePosition(Vector2 newDirection)
        {
            _body.MovePosition(newDirection);
        }
    }
}