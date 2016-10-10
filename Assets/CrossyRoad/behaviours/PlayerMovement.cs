﻿using UnityEngine;
using UnityEngine.Networking;
using Sa1;
using DG.Tweening;
using System.Collections;

namespace CrossyRoad {

	/// <summary>
	///     network player,
	/// </summary>
	public class PlayerMovement : NetworkBehaviour {

		public bool canMove = false;
		public bool isMoving = false;
		public float timeForMove = 0.2f;
		public float jumpHeight = 1.0f;

		Vector3 _targetPosition;

		/// <summary>
		/// Minimum grid X position allowed for player.
		/// </summary>
		public int minX = -4;

		/// <summary>
		/// Maximum grid X position allowed for player.
		/// </summary>
		public int maxX = 4;

		public void Start () {
			onReset();
		}

		public void onDie () {
			
		}

		public void onReset () {
			isMoving = false;
			_targetPosition = transform.position;
		}

		public void Update () {

			// if (!isMoving && transform.position != _targetPosition) {
			// 	StartCoroutine(moveTo(_targetPosition));
			// }

			if (!isLocalPlayer) return;
			// If player is moving, update the player position, else receive input from user.
			if (!isMoving) {
				// Update current to match integer position (not fractional).
				if (canMove) {
					HandleKeyInput();
					// if (Input.GetMouseButtonDown(0)) {
					// 	HandleMouseClick();
					// }
					HandleTouchPad();
				}
			}
		}

		void HandleTouchPad () {
			Vector2 inputDirection = BasicPanel.Instance.direction;
			// if (inputDirection != Vector2.zero) {
            //     Vector3 formatedDirection = GridObject.formateDirection(inputDirection.x, inputDirection.y);
            //     Move(formatedDirection);
			// }
			if (BasicPanel.Instance.padTapped) {
				CmdMove(GridObject.formateDirection(transform.forward));
			}
			else if (inputDirection != Vector2.zero) {
				CmdRotateTo(inputDirection);
			}
		}

		private void HandleMouseClick () {
			Camera.main.onRayHit(hitInfo => {
				var direction = hitInfo.point - transform.position;
				var formatedDirection = GridObject.formateDirection(direction);
                CmdMove(formatedDirection);
			});
		}

		private void HandleKeyInput () {

			if (Input.GetKeyUp(KeyCode.W)) {
				CmdMove(new Vector3(0, 0, 1));
			}
			else if (Input.GetKeyUp(KeyCode.S)) {
				CmdMove(new Vector3(0, 0, -1));
			}
			else if (Input.GetKeyUp(KeyCode.A)) {
				if (Mathf.RoundToInt(transform.position.x) > minX) {
					CmdMove(new Vector3(-1, 0, 0));
				}
			}
			else if (Input.GetKeyUp(KeyCode.D)) {
				if (Mathf.RoundToInt(transform.position.x) < maxX) {
					CmdMove(new Vector3(1, 0, 0));
				}
			}
		}

		[Command]
		void CmdRotateTo (Vector2 v) {
			transform.forward = new Vector3(v.x, 0, v.y);
		}

		[Command]
		private void CmdMove (Vector3 distance) {
            if (distance.x > 0 && Mathf.RoundToInt(transform.position.x) >= maxX) {
                return;
            }
            else if (distance.x < 0 && Mathf.RoundToInt(transform.position.x) <= minX) {
                return;
            }

			var newPosition = transform.position + distance;

			// Don't move if blocked by obstacle.
			// if (Physics.CheckSphere(newPosition, 0.1f)) return;
			if (GridObject.rayFind(transform.position, distance, 1) != null) return;

			// _targetPosition = newPosition;

			// do jump
			transform.DOKill(true);
			transform.DOJump(newPosition, jumpHeight, 1, timeForMove)
				.SetEase(Ease.InOutSine)
				.OnStart(() => {
					isMoving = true;
				})
				.OnComplete(()=>{
					transform.position = GridObject.round(transform.position);
					isMoving = false;
				});

			transform.forward = distance;
		}

		IEnumerator moveTo (Vector3 position) {
			isMoving = true;
			transform.DOKill(true);

			yield return transform.DOJump(position, jumpHeight, 1, timeForMove)
				.SetEase(Ease.InOutSine).WaitForCompletion();

			transform.position = GridObject.round(transform.position);
			_targetPosition = transform.position;
			isMoving = false;
		}
	}
}