using UnityEngine;

namespace PVZ.LawnMower
{
    public class LawnMowerController : LawnMower
    {
        [SerializeField] private float _rayDistance;
        private readonly Color rayColor = Color.yellow;
        private float _rayDistanceAdjusted;
        private const string _objectLayerMaskName = "Enemy";
        private bool _isActive;

        private void Start() => _rayDistanceAdjusted = _rayDistance - transform.position.x;

        void Update() { if (_isActive) base.Move(); }

        private void FixedUpdate()
        {
            if (!_isActive)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, _rayDistanceAdjusted, LayerMask.GetMask(_objectLayerMaskName));
                Debug.DrawRay(transform.position, Vector2.right * _rayDistance, rayColor);
                _isActive = hit.collider != null;
            }
        }
    }
}
