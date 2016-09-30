using UnityEngine;
using UnityEngine.Networking;

namespace CrossyRoad {

    /// <summary>
    ///     network player,
    /// </summary>
    public class Player : NetworkBehaviour {

        public string playerName;
        public string playerColor;

        /// <summary>
        ///     set self name, to distinct from other player
        /// </summary>
        void Start () {
            if (isLocalPlayer) {
                setSelfNameAndColor();
            }
            // set self name, and self color
        }

        void Update () {
            if (isLocalPlayer) {
                // move the camera
            }
        }

        public void setSelfNameAndColor () {
            
        }
    }
}