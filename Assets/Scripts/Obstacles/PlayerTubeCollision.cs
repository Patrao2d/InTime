using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTubeCollision : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(Suicide());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Chekcs for collision with the player and if he was sliding or not
        if (other.gameObject.CompareTag("Player") && Player.instance.isSliding == false)
        {
            // Case there was no shield remove half of the time form the player and destroy self
            if (Player.instance.isShieldActive == false)
            {
                CountdownTimer.instance.timer /= 2;
                Destroy(gameObject);
            }
            // Case the shield was active does nothing and removes the shield
            if (Player.instance.isShieldActive == true)
            {
                Player.instance.isShieldActive = false;
                Destroy(gameObject);
            }
        }
    }


    IEnumerator Suicide()
    {
        // Suicide after 60seconds
        yield return new WaitForSeconds(60f);
        Destroy(gameObject);
    }

}
