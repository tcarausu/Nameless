using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    internal QuestItem currentQuest { get; set; }

    public int highscore = 0;
    public GameObject pickup;
    public float timer = 0;
    public float timeToSpawn = 5f;

    void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // timer = Time.deltaTime;
        // if (timer >= timeToSpawn)
        // {
        //     timer = 0;
        //
        //     var spawnedPickup = Instantiate(pickup);
        //
        //     var randomX = Random.Range(-5, 5);
        //     var randomY = Random.Range(-5, 5);
        //
        //     spawnedPickup.transform.position = new Vector2(randomX, randomY);
        // }
    }
}