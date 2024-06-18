using UnityEngine;

namespace PVZ.Projectile
{
    public class Projectile : MonoBehaviour, IMove
    {
        [SerializeField] private int hitDamage;
        [SerializeField] private float movementSpeed;
        public int HitDamage => hitDamage;
        public float MovementSpeed => movementSpeed;

        public void Move()
        {
            transform.position += movementSpeed * Time.deltaTime * Vector3.right;
        }
    }
}
