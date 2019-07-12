using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        // Makes the gameobject rotate on the Z axis
        transform.Rotate(0, 0, 30 * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When the player touch this, actives the player shield and destroys itself
        if (other.gameObject.CompareTag("Player"))
        {
            Player.instance.isShieldActive = true;
            Destroy(gameObject);
        }
    }

}
