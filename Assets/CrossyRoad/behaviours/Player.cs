using UnityEngine;
using UnityEngine.Networking;
using Sa1;

namespace CrossyRoad {

	/// <summary>
	///     network player,
	/// </summary>
	public class Player : NetworkBehaviour {

		public string playerName;
		public string playerColor;

		Animator _animator;

		/// <summary>
		///     set self name, to distinct from other player
		/// </summary>
		void Start () {

			initMovementState(Vector3.zero);

		    if (isLocalPlayer) {
                // set self name, and self color
                setSelfNameAndColor();
		    }

			_animator = GetComponent<Animator>();
		}

		void Update () {
		    if (isLocalPlayer) {
		        // move the camera
		    }
		}

		public void setSelfNameAndColor () {

		}

		public void initMovementState (Vector3 position) {
			this.transform.position = position.withY(0.5f);
			GetComponent<PlayerMovement>().canMove = true;
		}

		public void onDie () {

		}

		public void onReset () {
			initMovementState(Vector3.zero);
		}

	}
}