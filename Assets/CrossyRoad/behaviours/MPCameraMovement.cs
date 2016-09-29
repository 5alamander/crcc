using System.Collections;
using UnityEngine;

public class MPCameraMovement : MonoBehaviour {

    private Vector3 _offset;
    private bool _startp;
    private GameObject _playerObject;

    public GameObject startObject;

    void Start () {
        _offset = transform.position - startObject.transform.position;
    }

    public void Update () {
        if (_startp) {
            transform.position = Vector3.Lerp(
                transform.position, _playerObject.transform.position + _offset, 0.8f * Time.deltaTime);
        }
    }

    public void StartToFollow (GameObject player) {
        _startp = true;
        _playerObject = player;
    }

    public void moveToPosition (Vector3 position) {
        transform.position = Vector3.Lerp(
            transform.position, position + _offset, 0.8f*Time.deltaTime);
    }
}