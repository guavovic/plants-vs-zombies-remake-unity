using UnityEngine;

namespace PVZ.LawnMower
{
    [System.Serializable]
    public class LawnMower : MonoBehaviour, IMove, IDamage
    {
        [SerializeField] private int hitDamage = 999;
        [SerializeField] private float movementSpeed;

        public void CauseDamage(Object target)
        {
            var enemy = target as Enemy.Enemy;
            enemy.ReceiveDamage(hitDamage);
        }

        public void Move()
        {
            transform.position = movementSpeed * Time.deltaTime * Vector2.right;
        }
    }
}
