using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : MonoBehaviour
{
    GameObject player;
    Vector3 moveDirection;
    Quaternion lookRotation;
    Rigidbody myRigidbody;
    public float turnSpeed;
    public float moveSpeed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigidbody = GetComponent<Rigidbody>();
    }

    void MoveTrailer(Vector3 point)
    {
        moveDirection = point - transform.position;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.LookAt(point);
    }

    void TurnTrailer(Vector3 force)
    {
        myRigidbody.AddTorque(force);
    }
}
