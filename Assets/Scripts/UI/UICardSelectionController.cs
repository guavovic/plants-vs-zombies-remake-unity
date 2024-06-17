using System;
using UnityEngine;
using UnityEngine.UI;

namespace PVZ.UI
{
    [Serializable]
    public class UICardSelectionController
    {
        [SerializeField] private Button playButton;

        public Button PlayButton { get => playButton; set => playButton = value; }
    }
}