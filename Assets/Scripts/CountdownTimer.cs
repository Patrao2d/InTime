using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    // Texts
    [SerializeField] private Text uiText;

    // Floats
    [SerializeField] private float mainTimer;
    [HideInInspector] public float timer;

    // Bools
    private bool canCount = true;


    private static CountdownTimer _instance;

    public static CountdownTimer instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        timer = mainTimer;
    }

    private void Start()
    {
        _instance = this;
    }


    void Update()
    {
        // When timer is > 0 counts the time based on the clock and send it to the ui
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uiText.text = Mathf.RoundToInt(timer).ToString("");
        }
        // If the timer gets to 0 sends the player to the lose menu scene
        else if (timer <= 0.0f)
        {
            canCount = false;
            SceneManager.LoadScene(2);
            uiText.text = "0.00";
            timer = 0.0f;
        }

    }



}
