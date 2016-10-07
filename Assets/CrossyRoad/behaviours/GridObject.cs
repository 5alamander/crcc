﻿using System;
using UnityEngine;
using System.Collections;
using Sa1;

/// <summary>
/// generic grid object
/// </summary>
public class GridObject : MonoBehaviour {

	public int height = 1;

	void Start () {

	}

	void Update () {

	}

	void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }

	// ** functions **

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

    public static Vector3 formateDirection (Vector3 direction) {
        return formateDirection(direction.x, direction.z);
    }
}
