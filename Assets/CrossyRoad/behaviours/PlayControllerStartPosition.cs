using UnityEngine;

public class PlayControllerStartPosition : MonoBehaviour {

    public PlayControllerStartPosition Instance { get; private set; }

    public Transform[] positions;

    void Awake () {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    ///     return a postion to start
    /// </summary>
    public Vector3 getPosition (int n = 0) {
        return positions[n].position;
    }
}
