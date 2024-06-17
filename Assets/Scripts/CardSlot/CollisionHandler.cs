using PVZ.GridSlot;
using System.Collections.Generic;
using UnityEngine;

namespace PVZ.Assets.Scripts.CardSlot
{
    internal class CollisionHandler
    {
        private readonly Vector2[] vector2s = { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
        private HashSet<GameObject> previouslyActivatedObjects = new HashSet<GameObject>();
        private ObjectDragging _objectDragging;
        private const string _containerLayerMaskName = "ContainerHit";
        private const float _raycastDistance = 15f;

        public void Ray(ObjectDragging objectDragging)
        {
            _objectDragging = objectDragging;

            HashSet<GameObject> activatedOpaqueObjects = new HashSet<GameObject>();
            RaycastHit2D[] colidersList;

            foreach (var direction in vector2s)
            {
                if ((colidersList = GetColliders(direction)) != null)
                {
                    foreach (var col in colidersList)
                    {
                        var objectContainer = col.collider.gameObject;
                        var opaqueObject = objectContainer.transform.GetChild(0).gameObject;
                        opaqueObject.gameObject.SetActive(true);
                        activatedOpaqueObjects.Add(opaqueObject.gameObject);
                    }
                }
            }

            foreach (GameObject obj in previouslyActivatedObjects)
            {
                if (!activatedOpaqueObjects.Contains(obj))
                    obj.SetActive(false);
            }

            previouslyActivatedObjects = activatedOpaqueObjects;
        }

        public void DesactivePreviouslyActivatedObjects()
        {
            foreach (GameObject obj in previouslyActivatedObjects)
                obj.SetActive(false);
        }

        private RaycastHit2D[] GetColliders(Vector2 direction)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(_objectDragging.transform.position, direction, _raycastDistance, LayerMask.GetMask(_containerLayerMaskName));
            Debug.DrawRay(_objectDragging.transform.position, direction, Color.red);
            return hit;
        }
    }
}
