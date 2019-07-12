using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCollision : MonoBehaviour
{
    private void Start()
    {

        StartCoroutine(Suicide());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks for collision with the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Removes half of the player time if there was no shield active
            if (Player.instance.isShieldActive == false)
            {
                CountdownTimer.instance.timer /=  2;
                Destroy(gameObject);
            }
            // If shield was active does nothing and removes the shield
            if (Player.instance.isShieldActive == true)
            {
                Player.instance.isShieldActive = false;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Suicide()
    {
        // Suicides after 60seconds
        yield return new WaitForSeconds(60f);
        Destroy(gameObject);
    }

}
