using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PVZ.CardSlot
{
    public class CardSlot : MonoBehaviour
    {
        [Header("--- CARD INSTANCE ---")]
        [SerializeField] private Card _card;

        [Header("--- SLOT REFERENCES ---")]
        [SerializeField] private GameObject priceTextContainer;
        [SerializeField] private TextMeshProUGUI priceTextMeshProUGUI;
        [SerializeField] private Image slotImage;
        [SerializeField] private Image slotFillImage;
        [SerializeField] private GameObject slotOpacityGameObject;

        private bool _isReloadingSlot;
        public Card Card { get => _card; set => _card = value; }
        public bool HasSelected { get; private set; }
        public bool CanBuy() => HasEnoughSuns() && !_isReloadingSlot;
        private bool HasEnoughSuns() => CollectableSunManager.CollectedSuns >= Card.Price;
        public bool IsEqual(Card card) => Card.Equals(card);
        public Vector3 GetPosition() => transform.position;
        public void ControlVisibilityOpacity(bool isPlayable)
        {
            if (isPlayable && Card.CardType != CardType.Shovel)
                slotOpacityGameObject.SetActive(!CanBuy());
        }

        public void ResetSlot()
        {
            _isReloadingSlot = false;
            slotOpacityGameObject.SetActive(false);
            StopCoroutine(ReloadingAnimationCoroutine());
            slotFillImage.gameObject.SetActive(false);
        }

        public void ConfigureSlot(Card card)
        {
            Card = card;
            ConfigureSlotImage();
            UpdateCardValueTextInSlot();

            void ConfigureSlotImage()
            {
                if (slotImage != null)
                {
                    slotImage.sprite = card.Sprite;
                    slotImage.color = Color.white;
                    slotImage.enabled = true;
                }
            }

            void UpdateCardValueTextInSlot()
            {
                if (priceTextMeshProUGUI != null)
                {
                    priceTextMeshProUGUI.text = Card.Price.ToString();
                    priceTextContainer.SetActive(true);
                }
            }
        }

        public void ClearSlot()
        {
            Card = default;
            ClearSlotImage();

            void ClearSlotImage()
            {
                if (slotFillImage != null)
                {
                    slotImage.sprite = null;
                    slotImage.color = Color.clear;
                }
            }
        }

        public void ToggleCardSlotState()
        {
            HasSelected = !HasSelected;
            slotOpacityGameObject.SetActive(HasSelected);
        }

        public void StartReloadingAnimtion()
        {
            StartCoroutine(ReloadingAnimationCoroutine());
            _isReloadingSlot = true;
        }

        private IEnumerator ReloadingAnimationCoroutine()
        {
            slotFillImage.gameObject.SetActive(true);
            slotFillImage.fillAmount = 1;
            float time = 0.0f;

            while (time < Card.CooldownToBuyInSeconds)
            {
                float progress = time / Card.CooldownToBuyInSeconds;
                slotFillImage.fillAmount = Mathf.Lerp(1, 0, progress);
                time += Time.deltaTime;
                yield return null;
            }

            slotFillImage.fillAmount = 0;
            _isReloadingSlot = false;
            slotFillImage.gameObject.SetActive(false);
        }
    }
}