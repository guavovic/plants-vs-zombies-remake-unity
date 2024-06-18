using PVZ.Projectile;
using UnityEngine;

namespace PVZ.ObjectShooter
{
    public class ObjectShooter : MonoBehaviour, IDamage
    {
        [SerializeField] private ProjectileController projectilePrefab;
        [SerializeField] private int life;
        [SerializeField] private float shotReloadTime;
        [SerializeField] private Transform shootOriginPosition;
        [Range(1, 8)][SerializeField] private float attackDistance;
        private GameManager _gameManager;

        public int Life { get => life; private set => life = value; }
        public float ShotReloadTime { get => shotReloadTime; private set => shotReloadTime = value; }
        public float AttackDistance { get => attackDistance; private set => attackDistance = value; }

        private void OnEnable() => _gameManager = GameManager.Instance;

        protected void Shoot()
        {
            Instantiate(projectilePrefab, shootOriginPosition.position, Quaternion.identity, _gameManager.UIManager.ProjetelParent);
        }

        public void CauseDamage(Object target)
        {
            var enemie = target as Enemy.Enemy;
            enemie.ReceiveDamage(projectilePrefab.HitDamage);
        }

        public void ReceiveDamage(int damage)
        {
            Life -= damage;

            if (Life <= 0 && gameObject != null)
                Destroy(gameObject);
        }
    }
}