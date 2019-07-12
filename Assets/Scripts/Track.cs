using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    // GameObjects
    public GameObject[] obstacles;

    // Vectors
    public Vector2 numberOfObstacles;

    // Ints
    private int newNumberOfObstacles;

    // Lists
    public List<GameObject> newObstacles;


    private static Track _instance;

    public static Track instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {/*
        // Gets a random number of obstacles for the track
        newNumberOfObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);

        // Based on the number of obstacles for the tracks, choses which ones to create
        for (int i = 0; i < newNumberOfObstacles; i++)
        {
            newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
            newObstacles[i].SetActive(false);
        }
        PositionateObstacles();*/
        CreateObstacles();
    }

    private void FixedUpdate()
    {
        //transform.Translate(-(trackSpeed) * Time.deltaTime, 0, 0);
    }

    void PositionateObstacles()
    {
        // Based on the obstacles that are chosen to create places them on the track
        for (int i = 0; i < newObstacles.Count; i++)
        {
            float posXMin = (-24f / newObstacles.Count) + (-24f / newObstacles.Count) * i;
            float posXMax = (24f / newObstacles.Count) + (24f / newObstacles.Count) * i + 1;
            newObstacles[i].transform.localPosition = new Vector3(Random.Range(posXMin, posXMax), 2.5f, 0);
            newObstacles[i].SetActive(true);

        }
    }

    void CreateObstacles()
    {
        // Gets a random number of obstacles for the track
        int newNumberOfObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);

        // Based on the number of obstacles for the tracks, choses which ones to create
        for (int i = 0; i < newNumberOfObstacles; i++)
        {
            newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
            newObstacles[i].SetActive(false);
        }
        PositionateObstacles();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When the players completes the tracks sets the obstacles to 0, clears the track, inscreases the player movement
        // changes the tracks position forward 300 in X axis and creates new obstacles
        if (other.gameObject.CompareTag("Player"))
        {
            newNumberOfObstacles = 0;
            newObstacles.Clear();

            Player.instance.speed += 0.5f;
            transform.position = new Vector3(transform.position.x + 300, 0, 0);
            CreateObstacles();
            //PositionateObstacles();

        }
    }



}