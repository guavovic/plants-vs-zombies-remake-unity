using PVZ.Enemy;
using UnityEngine;

namespace PVZ.Projectile
{
    public sealed class ProjectileController : Projectile
    {
        private void Update()
        {
            base.Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case Tags.Enemy:
                    collision.GetComponent<EnemyControlller>().ReceiveDamage(HitDamage);
                    Destroy(gameObject);
                    break;

                case Tags.Projectile_MapLimit:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}