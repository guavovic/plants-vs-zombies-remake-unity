using System;
using UnityEngine;

namespace PVZ.Enemy
{
    [Serializable]
    public sealed class EnemyControlller : Enemy
    {
        private void Update()
        {
            if (base.IsStopped)
                return;

            base.Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case Tags.ObjectShooter:
                    base.IsStopped = true;
                    base.CauseDamage(collision.GetComponent<ObjectShooter.ObjectShooter>());
                    break;

                case Tags.Enemy_MapLimit:
                    Destroy(gameObject);
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case Tags.ObjectShooter:
                    base.IsStopped = false;
                    break;
            }
        }
    }
}