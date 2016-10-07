using UnityEngine;
using UnityEngine.Networking;
using Sa1;

namespace CrossyRoad {

	/// <summary>
	///     network player,
	/// </summary>
	public class PlayerMovement : NetworkBehaviour {

		public bool canMove = false;

		public float timeForMove = 0.2f;

		public float jumpHeight = 1.0f;

		/// <summary>
		/// Minimum grid X position allowed for player.
		/// </summary>
		public int minX = -4;

		/// <summary>
		/// Maximum grid X position allowed for player.
		/// </summary>
		public int maxX = 4;

		private float elapsedTime;

		private Vector3 current;

		private Vector3 target;

		private float landHeight;

		private Rigidbody body;

		private int score;

		public void Start () {
			current = transform.position;
			IsMoving = false;
			landHeight = transform.position.y;

			body = GetComponentInChildren<Rigidbody>();
			score = 0;
		}

		public void Update () {

			if (!isLocalPlayer) return;

			// If player is moving, update the player position, else receive input from user.
			if (IsMoving) {
				MovePlayer();
			}
			else {
				// Update current to match integer position (not fractional).
				current = GridObject.round(transform.position); 

				if (canMove) {

					HandleKeyInput();

					// if (Input.GetMouseButtonDown(0)) {
					// 	HandleMouseClick();
					// }

					HandleTouchPad();
				}
			}

			score = Mathf.Max(score, (int)current.z);
		}

		void HandleTouchPad () {
			Vector2 inputDirection = BasicPanel.Instance.direction;
			if (inputDirection != Vector2.zero) {
                Vector3 formatedDirection = GridObject.formateDirection(inputDirection.x, inputDirection.y);
                Move(formatedDirection);
			}
			else if (BasicPanel.Instance.padTapped) {
				Move(GridObject.formateDirection(transform.forward));
			}
		}

		private void HandleMouseClick () {
			Camera.main.onRayHit(hitInfo => {
				var direction = hitInfo.point - transform.position;
				var formatedDirection = GridObject.formateDirection(direction);
                Move(formatedDirection);
			});
		}

		private void HandleKeyInput () {

			if (Input.GetKeyDown(KeyCode.W)) {
				Move(new Vector3(0, 0, 1));
			}
			else if (Input.GetKeyDown(KeyCode.S)) {
				Move(new Vector3(0, 0, -1));
			}
			else if (Input.GetKeyDown(KeyCode.A)) {
				if (Mathf.RoundToInt(current.x) > minX) {
					Move(new Vector3(-1, 0, 0));
				}
			}
			else if (Input.GetKeyDown(KeyCode.D)) {
				if (Mathf.RoundToInt(current.x) < maxX) {
					Move(new Vector3(1, 0, 0));
				}
			}
		}

		private void Move (Vector3 distance) {
            if (distance.x > 0 && Mathf.RoundToInt(current.x) >= maxX) {
                return;
            }
            else if (distance.x < 0 && Mathf.RoundToInt(current.x) <= minX) {
                return;
            }

			var newPosition = current + distance;

			// Don't move if blocked by obstacle.
			if (Physics.CheckSphere(newPosition.withY(1), 0.1f)) return;

			target = newPosition;

			IsMoving = true;
			elapsedTime = 0;
			body.isKinematic = true;

			transform.forward = distance;
		}

		private void MovePlayer () {
			elapsedTime += Time.deltaTime;

			var weight = (elapsedTime < timeForMove) ? (elapsedTime / timeForMove) : 1;
			var x = Lerp(current.x, target.x, weight);
			var z = Lerp(current.z, target.z, weight);
			var y = Sinerp(current.y, landHeight + jumpHeight, weight);

			var result = new Vector3(x, y, z);
			transform.position = result; // note to self: why using transform produce better movement?
										 //body.MovePosition(result);

			if (result == target) {
				IsMoving = false;
				current = target;
				body.isKinematic = false;
				body.AddForce(0, -10, 0, ForceMode.VelocityChange);

			}
		}

		private float Lerp (float min, float max, float weight) {
			return min + (max - min) * weight;
		}

		private float Sinerp (float min, float max, float weight) {
			return min + (max - min) * Mathf.Sin(weight * Mathf.PI);
		}

		public bool IsMoving { get; private set; }

		public string MoveDirection {
			get {
				if (IsMoving) {
					var dx = target.x - current.x;
					var dz = target.z - current.z;
					if (dz > 0) {
						return "north";
					}
					if (dz < 0) {
						return "south";
					}
					if (dx > 0) {
						return "west";
					}
					return "east";
				}
				return null;
			}
		}
	}
}