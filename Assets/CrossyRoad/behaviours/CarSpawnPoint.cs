using UnityEngine;
using UnityEngine.Networking;

namespace CrossyRoad {

    public class CarSpawnPoint : NetworkBehaviour {

        public GameObject Car;

        void Start () {
            
        }

        void Update () {

            // spawn the car in server side
            if (isServer) {
                // spawn Car
            }

        }

    }
}