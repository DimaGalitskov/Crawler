using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    GameObject player;
    Vector3 moveDirection;
    Quaternion lookRotation;
    public float turnSpeed;
    public float moveSpeed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("GrowChain");
        }
    }
}
