using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PVZ.ObjectShooter
{
    public sealed class ObjectShooterController : ObjectShooter
    {
        private readonly Color rayColor = Color.red;
        private float _rayDistance;
        private const string _objectLayerMaskName = "Enemy";
        private bool _canShoot = true;

        private void Start() => _rayDistance = base.AttackDistance - transform.position.x;

        private async void FixedUpdate()
        {
            if (!_canShoot)
                return;

            if (HasEnemy())
            {
                _canShoot = false;
                base.Shoot();
                await Task.Delay(TimeSpan.FromSeconds(base.ShotReloadTime));
                _canShoot = true;
            }
        }

        private bool HasEnemy()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, _rayDistance, LayerMask.GetMask(_objectLayerMaskName));
            Debug.DrawRay(transform.position, Vector2.right * _rayDistance, rayColor);
            return hit.collider != null;
        }
    }
}