using UnityEngine;

namespace PVZ.Sun
{
    public class CollectableSun : MonoBehaviour
    {
        [SerializeField] private int valueToBeReceived;
        [SerializeField] private float fallSpeed;
        [SerializeField] private float rotationSpeed;

        public int ValueToBeReceived { get => valueToBeReceived; set => valueToBeReceived = value; }

        public void FallingMovement() => transform.position += fallSpeed * Time.deltaTime * Vector3.down;
        public void Rotate() => transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}