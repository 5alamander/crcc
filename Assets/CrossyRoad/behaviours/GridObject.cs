using System;
using UnityEngine;
using System.Collections;
using Sa1;
using DG.Tweening;

/// <summary>
/// generic grid object
/// </summary>
public class GridObject : MonoBehaviour {

    public static class Tag {
        public const string player = "Player";
        public const string item = "Item";
        public const string block = "Block";
        public const string land = "Land";
        public const string river = "River";
    }

    public static class Layer {
        public static readonly int grid = LayerMask.GetMask("GridObject");
        public static readonly int block = LayerMask.GetMask("Block");
    }

	public int height = 1;

	void Start () {
        transform.position = round(transform.position);
	}

	void Update () {
        // transform.position = Vector3.Lerp(transform.position, _gridPosition, 0.5f);
	}

    public bool jumpTo (Vector3 endPosition, float jumpHeight) {
        // check if the end position have the GridObject
        transform.DOJump(endPosition, jumpHeight, 1, 0.5f);
        return true;
    }

	void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireCube(transform.position, new Vector3(1, height, 1));
    }

    // ** params **
    public const int onLandY = 1;

	// ** functions **

    // grid interact
    public static bool take (GridObject g1, GridObject g2) {
        throw new NotImplementedException();
    }

    public static bool cast (GridObject g1, GridObject g2) {
        throw new NotImplementedException();
    }

    public static bool cast (GridObject g1) {
        throw new NotImplementedException();
    }

    // find and set
    public static Transform find (Vector3 position) {
        var colls = Physics.OverlapBox(position, Vector3.one * 0.1f, Quaternion.identity, GridObject.Layer.grid);
        return colls.Length > 0 ? colls[0].transform : null;
    }

    public static Transform findLand (Vector3 position) {
        return find(position.withY(onLandY));
    }

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
