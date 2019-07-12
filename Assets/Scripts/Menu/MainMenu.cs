using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Rigidbodys
    private Rigidbody2D rb;

    // Animator
    private Animator anim;

    // Booleans
    private bool nothingSelected = true;
    private bool quitSelected = false;
    private bool playSelected = false;

    // Gameobjects
    [SerializeField] private GameObject imageClock;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = imageClock.GetComponent<Animator>();
    }

  
    public void PlaySelected()
    {
        // Case the player hasnt selected anything yet plays the animation 
        if (nothingSelected == true)
        {
            nothingSelected = false;
            playSelected = true;
            anim.Play("StartToPlay");
        }
        // If the player had quit selected plays the animation
        if (quitSelected == true)
        {
            quitSelected = false;
            playSelected = true;
            anim.Play("QuitToPlay");
        }
    }

    public void QuitSelected()
    {
        //Case the player hasnd selected anything yet plays the animation
        if (nothingSelected == true)
        {
            nothingSelected = false;
            quitSelected = true;
            anim.Play("StartToQuit");
        }
        // If the player had play select plays the animation
        if (playSelected == true)
        {
            quitSelected = true;
            playSelected = false;
            anim.Play("PlayToQuit");
        }

    }


    public void Play()
    {
        // Load the play scene
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        // Closes the application
        Application.Quit();
    }

    public void Menu()
    {
        // Open the main menu 
        SceneManager.LoadScene(0);
        ZoneMenu.instance.Resume();
    }

}
