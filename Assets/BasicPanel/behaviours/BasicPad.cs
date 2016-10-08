using UnityEngine;
using UnityEngine.EventSystems;

namespace Sa1 {

public class BasicPad : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {
    
    public Vector2 direction {get; private set;}
    public Vector2 lastDirection {get; private set;}
    public bool padTapped {get; private set;}
    
    public RectTransform padBottom;
    public RectTransform padHandler;

    public bool hasHandler = true;
    
    protected virtual void LateUpdate() {
        padTapped = false;
        lastDirection = direction;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        // Debug.Log("begin drag" + eventData.pressPosition);
        if (!hasHandler) return;
        padBottom.gameObject.SetActive(true);
        padHandler.gameObject.SetActive(true);
        // set position
        padBottom.position = eventData.pressPosition;
        padHandler.position = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        direction = (eventData.position - eventData.pressPosition).normalized;
        // Debug.Log("drag" + direction);
        if (!hasHandler) return;
        padHandler.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        // Debug.Log("end drag" + eventData.position);
        direction = Vector2.zero;
        if (!hasHandler) return;
        padBottom.gameObject.SetActive(false);
        padHandler.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) {
        padTapped = true;
    }
}

}