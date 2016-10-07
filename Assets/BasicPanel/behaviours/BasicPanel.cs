using UnityEngine;
using UnityEngine.EventSystems;

public class BasicPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {
    private static BasicPanel _instance;
    public static BasicPanel Instance {
        get {return _instance;}
    }

    public RectTransform padBottom;
    public RectTransform padHandler;

    public Vector2 direction {get; private set;}
    public bool padTapped {get; private set;}

    public bool toggleButtonA {get; private set;}
    public bool toggleButtonB {get; private set;}

    // Use this for initialization
    void Start() {
        _instance = this;
    }

    void LateUpdate() {
        // refresh the toggle
        toggleButtonA = false;
        toggleButtonB = false;
        padTapped = false;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        // Debug.Log("begin drag" + eventData.pressPosition);
        padBottom.gameObject.SetActive(true);
        padHandler.gameObject.SetActive(true);
        // set position
        padBottom.position = eventData.pressPosition;
        padHandler.position = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        direction = (eventData.position - eventData.pressPosition).normalized;
        // Debug.Log("drag" + direction);
        padHandler.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        // Debug.Log("end drag" + eventData.position);
        direction = Vector2.zero;
        padBottom.gameObject.SetActive(false);
        padHandler.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) {
        padTapped = true;
    }

    public void PressButtonA() {
        toggleButtonA = true;
    }

    public void PressButtonB() {
        toggleButtonB = true;
    }
}