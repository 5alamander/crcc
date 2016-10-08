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

        bool _isHoldObject = false;
        GameObject _theObejct = null;

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
                var dirct = _actionPad.lastDirection != Vector2.zero
                    ? GridObject.formateDirection(_actionPad.lastDirection)
                    : transform.forward;
                if (_isHoldObject) { // cast it
                    _isHoldObject = !cast(dirct);
                }
                else { // take it
                    _isHoldObject = take(dirct);
                }
            }
        }

        public bool take (Vector3 direction) {
            // get object in the direction
            // check if it can be hold
            var trans = GridObject.rayFind(transform.position, direction, 1);
            if (!trans) return false;
            if (trans.tag == GridObject.type.item || trans.tag == GridObject.type.item) {
                _theObejct = trans.gameObject;
                trans.SetParent(transform);
                trans.localPosition = Vector3.zero.withY(1); // TODO calculate with height
                return true;
            }
            return false;
        }

        public bool cast (Vector3 direction) {
            _theObejct.transform.SetParent(null, true);
            _theObejct.transform.position = transform.localPosition + direction;
            return true;
        }

    }

}