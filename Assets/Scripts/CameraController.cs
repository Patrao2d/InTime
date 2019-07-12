using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Rigidbodys
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //transform.Translate(Player.instance.speed * Time.deltaTime, 0, 0);
        // Adds a constant velocity to the camera based on the player current speed
        rb.velocity = new Vector2(Player.instance.speed * 0.5f, rb.velocity.y);
    }
}
