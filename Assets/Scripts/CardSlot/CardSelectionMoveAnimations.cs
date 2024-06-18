using System.Threading.Tasks;
using UnityEngine;

namespace PVZ.CardSlot
{
    public enum Target
    {
        Initial,
        Final
    }

    internal sealed class CardSelectionMoveAnimations
    {
        private readonly GameManager _gameManager;
        private CardSelectionSlotController _cardSelectionSlotController;
        private Vector3 startPosition;
        private Vector3 finalPosition;

        public CardSelectionMoveAnimations(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public async Task MoveTo(CardSelectionSlotController cardSelectionSlot, Target target)
        {
            _cardSelectionSlotController = cardSelectionSlot;
            startPosition = _gameManager.CardSelectionGridSlotManager.GetSelectedController(cardSelectionSlot.Card).GetPosition();
            finalPosition = _gameManager.CardSelectionSlotManager.GetSelectionSlot(cardSelectionSlot.Card).GetPosition();
            await HandleStartAnimation(target == Target.Initial);
        }

        private async Task HandleStartAnimation(bool moveToFinal)
        {
            await MoveCardToPosition(GetMoveDirectionPosition(moveToFinal), GetMoveDirectionPosition(!moveToFinal));
        }

        private async Task MoveCardToPosition(Vector3 initialPosition, Vector3 targetPosition)
        {
            _cardSelectionSlotController.gameObject.SetActive(true);

            const float duration = 0.25f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                _cardSelectionSlotController.gameObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                await Task.Yield();
            }

            _cardSelectionSlotController.gameObject.transform.position = targetPosition;
        }

        private Vector3 GetMoveDirectionPosition(bool isMoveToFinal) => isMoveToFinal ? startPosition : finalPosition;
    }
}