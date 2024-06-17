using System;
using UnityEngine;

namespace PVZ.UI
{
    [Serializable]
    public struct Panels
    {
        [SerializeField] private string name;
        [SerializeField] private PanelType type;
        [SerializeField] private GameObject gameObject;

        public readonly string Name => name;
        public readonly PanelType Type => type;
        public readonly GameObject GameObject => gameObject;
    }
}