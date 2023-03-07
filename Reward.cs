using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    GameObject player;
    GameObject gameController;
    Vector3 moveDirection;
    Quaternion lookRotation;
    public float turnSpeed;
    public float moveSpeed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("GrowChain");
            gameController.SendMessage("SpawnReward");
            Destroy(gameObject);
        }
    }
}
