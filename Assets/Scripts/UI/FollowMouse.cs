using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class FollowMouse : MonoBehaviour
    {
        private RectTransform rectTransform;

        private void UpdatePosition(Vector2 position)
        {
            rectTransform.position = position;
        }

        private void Start()
        {
            rectTransform = ((RectTransform)transform);
            Controls.mousePosition += UpdatePosition;
        }

        private void OnDestroy()
        {
            Controls.mousePosition -= UpdatePosition;
        }
    }
}