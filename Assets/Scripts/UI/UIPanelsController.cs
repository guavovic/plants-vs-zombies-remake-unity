using System;
using System.Collections.Generic;
using UnityEngine;

namespace PVZ.UI
{
    [Serializable]
    public class UIPanelsController
    {
        [SerializeField] private List<Panels> panels;

        public void TogglePanelsState(params PanelType[] panels)
        {
            foreach (var selected in GetSelecteds(panels))
            {
                selected.SetActive(!selected.activeSelf);
            }
        }

        private List<GameObject> GetSelecteds(params PanelType[] types)
        {
            List<GameObject> gameObjects = new List<GameObject>();

            foreach (var type in types)
            {
                foreach (var panel in panels)
                {
                    if (type == panel.Type)
                        gameObjects.Add(panel.GameObject);
                }
            }

            return gameObjects;
        }
    }
}