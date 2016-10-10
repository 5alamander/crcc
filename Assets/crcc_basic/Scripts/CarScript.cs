using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour {

    // =============================
    // TODO Genericize Car and Trunk
    // =============================

    /// <summary>
    /// The X-speed of car, in units per second.
    /// </summary>
    public float speedX = 1.0f;

    private Rigidbody playerBody;

    public void Update()
    {
        transform.position += new Vector3(speedX * Time.deltaTime, 0.0f, 0.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        // When collide with player, flatten it!
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("onDie", "car-front");
        }
    }
}
