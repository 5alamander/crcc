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
		
		PlayerMovement _movement;

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
			_movement = GetComponent<PlayerMovement>();
		}

		void Update () {
		    if (isLocalPlayer) {
		        // move the camera
		    }
		}

		public void setSelfNameAndColor () {

		}

		public void initMovementState (Vector3 position) {
			this.transform.position = position.withY(1f);
			GetComponent<PlayerMovement>().canMove = true;
		}

		public void onDie () {
			// set y as 0.1
            transform.localScale = transform.localScale.withY(0.1f);
			_movement.canMove = false;
		}

		public void onReset () {
			initMovementState(Vector3.zero);
			transform.localScale = Vector3.one;
		}

	}
}