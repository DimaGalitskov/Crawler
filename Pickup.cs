using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    GameObject player;
    GameObject gameController;
    Rigidbody myRigidBody;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        myRigidBody = GetComponent<Rigidbody>();
        Vector3 direction = (Vector3.forward + Vector3.up) * 10;
        myRigidBody.AddRelativeForce(direction, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("GrowChain");
            Destroy(gameObject);
        }
    }
}
