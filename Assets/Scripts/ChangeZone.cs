using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if player collided with the object then open the upgrade menu, destroys this
        if (other.gameObject.CompareTag("Player"))
        {
            ZoneMenu.instance.Pause();
            Destroy(transform.parent.gameObject);
        }
    }
}
