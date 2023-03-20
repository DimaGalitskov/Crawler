using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject pickup;
    public GameObject target;
    public TextMeshProUGUI playerScoreUI;
    public float spawnRangeX;
    public float spawnRangeZ;
    public float spawnSize;
    public GameObject player;
    public GameObject spawn;
    GameObject[] pickups;
    int playerScore = 0;

    private void Start()
    {
        UpdateScore();
        SpawnPlayer();
    }

    void Reset()
    {
        Sweep();
        SpawnPlayer();
    }

    void UpdateScore()
    {
        var step = 10;
        playerScore += step;
        playerScoreUI.SetText("Score: " + playerScore);
    }

    void SpawnPlayer()
    {
        playerScore = 0;
        UpdateScore();
        Instantiate(player, spawn.transform.position, player.transform.rotation);
    }

    void Sweep()
    {
        pickups = GameObject.FindGameObjectsWithTag("Pickup");
        foreach (var item in pickups)
        {
            item.SendMessage("RemoveObject");
        }
    }

}
