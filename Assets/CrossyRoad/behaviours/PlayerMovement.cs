using UnityEngine;
using Sa1;

namespace CrossyRoad {

	/// <summary>
	///     network player,
	/// </summary>
	public class PlayerMovement : MonoBehaviour {

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

		MPCameraMovement cameraMovement;

		public void Start () {
			current = transform.position;
			IsMoving = false;
			landHeight = transform.position.y;

			body = GetComponentInChildren<Rigidbody>();
			score = 0;

			cameraMovement = Camera.main.GetComponent<MPCameraMovement>();
		}

		public void Update () {

			// move the camera to the local player
			cameraMovement.moveToPosition(transform.position);

			// If player is moving, update the player position, else receive input from user.
			if (IsMoving) {
				MovePlayer();
			}
			else {
				// Update current to match integer position (not fractional).
				current = new Vector3(
					Mathf.Round(transform.position.x),
					Mathf.Round(transform.position.y),
					Mathf.Round(transform.position.z));

				if (canMove) {

					HandleKeyInput();

					if (Input.GetMouseButtonDown(0)) {
						HandleMouseClick();
					}
				}
			}

			score = Mathf.Max(score, (int)current.z);
		}

		private void HandleMouseClick () {

			RaycastHit hit;
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit)) {
				var direction = hit.point - transform.position;
				var x = direction.x;
				var z = direction.z;

				// North = abs(z) > abs(x), z > 0
				// South = abs(z) > abs(x), z < 0
				// East  = abs(z) < abs(x), x > 0
				// West  = abs(z) < abs(x), x < 0

				if (Mathf.Abs(z) > Mathf.Abs(x)) {
					if (z > 0) {
						Move(new Vector3(0, 0, 1));
					}
					else // (z < 0)
					{
						Move(new Vector3(0, 0, -1));
					}
				}
				else // (Mathf.Abs(z) < Mathf.Abs(x))
				{
					if (x > 0) {
						if (Mathf.RoundToInt(current.x) < maxX) {
							Move(new Vector3(1, 0, 0));
						}
					}
					else // (x < 0)
					{
						if (Mathf.RoundToInt(current.x) > minX) {
							Move(new Vector3(-1, 0, 0));
						}
					}
				}
			}
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