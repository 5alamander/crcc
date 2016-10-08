using UnityEngine;
using UnityEngine.EventSystems;

namespace Sa1{
    
public class BasicPanel : BasicPad {
    private static BasicPanel _instance;
    public static BasicPanel Instance {
        get {return _instance;}
    }

    public bool toggleButtonA {get; private set;}
    public bool toggleButtonB {get; private set;}

    // Use this for initialization
    void Start() {
        _instance = this;
    }

    protected override void LateUpdate() {
        base.LateUpdate();
        // refresh the toggle
        toggleButtonA = false;
        toggleButtonB = false;
    }

    public void PressButtonA() {
        toggleButtonA = true;
    }

    public void PressButtonB() {
        toggleButtonB = true;
    }
}

}
