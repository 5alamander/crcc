using System;
using UnityEngine;
using System.Collections;
using Sa1;

/// <summary>
/// generic grid object
/// </summary>
public class GridObject : MonoBehaviour {

    public static class type {
        public const string player = "Player";
        public const string item = "Item";
        public const string block = "Block";
    }

	public int height = 1;

	void Start () {
        transform.position = round(transform.position);
	}

	void Update () {

	}

	void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireCube(transform.position, new Vector3(1, height, 1));
    }

	// ** functions **

    public const int onLandY = 1;

    // find and set
    public static Transform rayFind (Vector3 position, Vector3 direction, int distance) {
        var ray = new Ray(position, direction);
        return ray.pickObject(distance);
    }

    // round and format
    public static Vector3 round (Vector3 position) {
        return new Vector3(
            Mathf.Round(position.x),
            Mathf.Round(position.y),
            Mathf.Round(position.z)
        );
    }

    public static Vector3 formateDirection (float x, float y) {
        // North = abs(z) > abs(x), z > 0
        // South = abs(z) > abs(x), z < 0
        // East  = abs(z) < abs(x), x > 0
        // West  = abs(z) < abs(x), x < 0
        if (Mathf.Abs(y) > Mathf.Abs(x)) {
            if (y > 0) {
                return new Vector3(0, 0, 1);
            }
            else { // (y < 0)
                return new Vector3(0, 0, -1);
            }
        }
        else { // (Mathf.Abs(y) < Mathf.Abs(x))
            if (x > 0) {
                return new Vector3(1, 0, 0);
            }
            else { // (x < 0)
                return new Vector3(-1, 0, 0);
            }
        }
    }

    public static Vector3 formateDirection (Vector2 direction) {
        return formateDirection(direction.x, direction.y);
    }

    public static Vector3 formateDirection (Vector3 direction) {
        return formateDirection(direction.x, direction.z);
    }
}
