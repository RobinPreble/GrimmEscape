using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private bool isDragging = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Move the UI object with the mouse
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 localPoint);
            rectTransform.localPosition = localPoint;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }
}
