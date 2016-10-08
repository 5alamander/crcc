using UnityEngine;
using UnityEngine.Networking;
using System;
using Sa1;

namespace CrossyRoad {

	/// <summary>
	///     network player,
	/// </summary>
	public class PlayerAction : NetworkBehaviour {

        BasicPad _actionPad;

        bool _holdObject = false;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start() {
            _actionPad = BasicPanel.Instance[1].GetComponent<BasicPad>();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update() { 
           if (_actionPad.padTapped) {
               // get object in front of player
           } 
        }

        public bool take (Vector3 direction) {
            // get object in the direction
            // check if it can be hold
            throw new NotImplementedException();
        }

        public bool cast (Vector3 direction) {
            throw new NotImplementedException();
        }

    }

}