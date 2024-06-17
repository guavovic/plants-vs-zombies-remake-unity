
using System.Collections;
using UnityEngine;

namespace PVZ.Enemy
{
    [System.Serializable]
    public class Enemy : MonoBehaviour, IMove, IDamage
    {
        [SerializeField] private int life;
        [SerializeField] private float movementSpeed;
        [SerializeField] private int hitDamage;
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private float attackSpeedInSeconds;

        public bool IsStopped { get; set; }
        public EnemyType EnemyType => enemyType;

        public void Move()
        {
            transform.position += movementSpeed * Time.deltaTime * -Vector3.right;
        }

        public void CauseDamage(Object target)
        {
            StartCoroutine(CauseDamageCoroutine(target as ObjectShooter.ObjectShooter));
        }

        private IEnumerator CauseDamageCoroutine(ObjectShooter.ObjectShooter objectShooter)
        {
            while (objectShooter.Life > 0)
            {
                yield return new WaitForSeconds(attackSpeedInSeconds);
                objectShooter.ReceiveDamage(hitDamage);
                yield return null;
            }
        }

        public void ReceiveDamage(int damage)
        {
            life -= damage;

            if (life <= 0)
                Destroy(gameObject);
        }
    }
}
