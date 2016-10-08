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
		MPCameraMovement _cameraMovement;

		/// <summary>
		///     set self name, to distinct from other player
		/// </summary>
		void Start () {
			_animator = GetComponent<Animator>();
			_movement = GetComponent<PlayerMovement>();
			_cameraMovement = Camera.main.GetComponent<MPCameraMovement>();

			initMovementState(Vector3.zero);

		    if (isLocalPlayer) {
                // set self name, and self color
                setSelfNameAndColor();
		    }
		}

		void Update () {
		    if (isLocalPlayer) {
                // move the camera to the local player
                _cameraMovement.moveToPosition(transform.position);
		    }
		}

		public void setSelfNameAndColor () {

		}

		public void initMovementState (Vector3 position) {
			this.transform.position = position.withY(1f);
			_movement.canMove = true;
		}

		public void onDie () {
			// set the look
            transform.localScale = transform.localScale.withY(0.1f);
			// set the movement state
			_movement.canMove = false;

			Invoke("onReset", 2f);
		}

		public void onDrown () {
			_movement.canMove = false;	
			Invoke("onReset", 1f);
		}

		public void onReset () {
			// set the look
			transform.localScale = Vector3.one;
			// set the movement state
			initMovementState(Vector3.zero);
		}

	}
}