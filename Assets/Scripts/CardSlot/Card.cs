using System;
using UnityEngine;

namespace PVZ.CardSlot
{
    [Serializable]
    public struct Card
    {
        [SerializeField] private string name;
        [SerializeField] private CardType cardType;
        [SerializeField] private Sprite sprite;
        [SerializeField] private int price;
        [SerializeField] private int cooldownToBuyInSeconds;
        [SerializeField] private GameObject objectDrag;
        [SerializeField] private GameObject objectInGame;

        public readonly string Name => name;
        public readonly CardType CardType => cardType;
        public readonly Sprite Sprite => sprite;
        public readonly int Price => price;
        public readonly int CooldownToBuyInSeconds => cooldownToBuyInSeconds;
        public readonly GameObject ObjectDrag => objectDrag;
        public readonly GameObject ObjectInGame => objectInGame;
    }
}