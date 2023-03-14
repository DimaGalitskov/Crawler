using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontBumper : MonoBehaviour
{
    GameObject player;
    GameObject gameController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            player.SendMessage("ObstacleCollision");
            Destroy(gameObject);
        }
    }
}
