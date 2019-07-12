using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Suicide());
    }
    void Update()
    {
        // Moves the player on the X axis
        transform.Translate(-5f * Time.deltaTime, 0, 0, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks for collision with the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Case the player hadnt shield and had more the 100 time sets it to 20 else kills it
            if (Player.instance.isShieldActive == false)
            {
                
                if (CountdownTimer.instance.timer > 100)
                {
                    CountdownTimer.instance.timer = 20;
                    Destroy(gameObject);
                }
                else
                {
                    SceneManager.LoadScene(2);
                }
            }
            // If the player had shield removes it and destroy self
            if (Player.instance.isShieldActive == true)
            {
                Player.instance.isShieldActive = false;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Suicide()
    {
        // Suicides after 30seconds
        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
    }


}
