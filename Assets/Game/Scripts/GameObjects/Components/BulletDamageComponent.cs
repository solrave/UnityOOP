using UnityEngine;

namespace Game.Scripts.Components
{
    public class BulletDamageComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;
        public int Damage => _damage;
    }
}