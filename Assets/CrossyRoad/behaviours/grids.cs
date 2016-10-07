using System;
using UnityEngine;

class grids {

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