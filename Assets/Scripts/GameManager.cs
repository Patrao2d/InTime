using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Floats
    [HideInInspector] public float score;
    [HideInInspector] public float monetTime;

    // Ints
    [HideInInspector] public int currentZone;

    // Texts
    [SerializeField] private Text zoneText;

    private static GameManager _instance;
    public static GameManager instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
        currentZone = 24;
        ZoneMenu.instance.Resume();
    }

    void Start()
    {
        CurrentZone();
    }

    void Update()
    {
        // Cheat to reset the current lvl
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            ReloadLevel();
        }
        // Checks if the player go to zone1 and won the game
        if (currentZone == 1)
        {
            SceneManager.LoadScene(3);
        }
        // Cheat to give the player 1000 time
        if (Input.GetKeyDown(KeyCode.T))
        {
            CountdownTimer.instance.timer += 1000;
        }
    }

    public void ReloadLevel()
    {
        // Reloads the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


    public void CurrentZone()
    {   
        // Send the current scene to the UI
        zoneText.text = currentZone.ToString();
    }
}

