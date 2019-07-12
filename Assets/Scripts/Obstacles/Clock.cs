using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // Animators
    private Animator anim;

    // Bools
    private bool isActive;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isActive = false;
        StartCoroutine(Suicide());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When the player touches the clock, give the player X amount of time, starts the animation and the coroutine
        if (other.gameObject.CompareTag("Player") && isActive == false)
        {
            isActive = true;          
            CountdownTimer.instance.timer += 50 * (ZoneMenu.instance.clockLevel);
            ClockAnim();
            StartCoroutine(ClockCooldown());
        }
    }

    void ClockAnim()
    {
        Debug.Log("tick tak");
        anim.Play("Active");
    }

    IEnumerator ClockCooldown()
    {
        // Give a cooldown of 5seconds to the clock so the player can't spam the same clock to get the rewards
        yield return new WaitForSeconds(5f);
        isActive = false;
    }

    IEnumerator Suicide()
    {
        // Destroys the clock after 60seconds
        yield return new WaitForSeconds(60f);
        Destroy(gameObject);
    }
}
