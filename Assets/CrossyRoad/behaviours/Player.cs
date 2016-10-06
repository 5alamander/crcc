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

		/// <summary>
		///     set self name, to distinct from other player
		/// </summary>
		void Start () {

			initState(Vector3.zero);

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

		public void initState (Vector3 position) {
			this.transform.position = position.withY(0.5f);
		}

	}
}