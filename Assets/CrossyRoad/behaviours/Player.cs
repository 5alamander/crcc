using UnityEngine;
using UnityEngine.Networking;

namespace CrossyRoad {

    /// <summary>
    ///     network player,
    /// </summary>
    public class Player : NetworkBehaviour {

        /// <summary>
        ///     set self name, to distinct from other player
        /// </summary>
        void Start () {
            
            // set self name, and self color
        }

        void Update () {
            if (isLocalPlayer) {
                // move the camera
            }
        }
    }
}