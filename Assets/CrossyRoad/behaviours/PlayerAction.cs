using UnityEngine;
using UnityEngine.Networking;
using System;
using Sa1;
using DG.Tweening;

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

        void onDie () {
            onReset();
        }

        void onDrown () {
            onReset();
        }

        void onReset () {
            _isHoldObject = false;
            _theObejct = null;
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

            if (_isHoldObject) {
                _theObejct.transform.position = transform.position.withY(
                    transform.position.y + 1);
            }
        }

        void LateUpdate () {
            
        }

        public bool take (Vector3 direction) {
            // get object in the direction
            // check if it can be hold
            var trans = GridObject.rayFind(transform.position, direction, 1);
            if (!trans) return false;
            if (trans.tag == GridObject.Tags.item || trans.tag == GridObject.Tags.item) {
                _theObejct = trans.gameObject;
                return true;
            }
            return false;
        }

        public bool cast (Vector3 direction) {
            _theObejct.transform.DOKill(true);
            _theObejct.transform.DOJump(transform.localPosition + direction, 1f, 1, 0.3f)
                .SetEase(Ease.InOutSine);
            return true;
        }

    }

}