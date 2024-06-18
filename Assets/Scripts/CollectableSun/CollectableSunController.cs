using UnityEngine;
using UnityEngine.EventSystems;

namespace PVZ.Sun
{
    public class CollectableSunController : CollectableSun, IPointerClickHandler
    {
        private float _dropToYPosition;
        private const float _minDropY = 2f;
        private const float _maxDropY = -3f;
        private CollectableSunManager collectableSunManager;

        private void Start()
        {
            collectableSunManager = CollectableSunManager.Instance;
            SetRandomDropToYPosition();
        }

        void Update()
        {
            base.Rotate();

            if (transform.position.y >= _dropToYPosition)
                base.FallingMovement();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            CollectCollectableSun();
            Destroy(gameObject);


            Debug.Log("Sol coletado com sucesso! Valor coletado: " + base.ValueToBeReceived);
        }

        public class YourScript : MonoBehaviour, IPointerClickHandler
        {
            public void OnPointerClick(PointerEventData eventData)
            {
                GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

                Destroy(clickedObject);

                Debug.Log("Objeto clicado: " + clickedObject.name);
            }
        }

        private void CollectCollectableSun() => collectableSunManager.SetCollectedSuns(base.ValueToBeReceived);

        private void SetRandomDropToYPosition() => _dropToYPosition = Random.Range(_minDropY, _maxDropY);
    }
}
