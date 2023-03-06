using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : MonoBehaviour
{
    GameObject player;
    Vector3 moveDirection;
    Quaternion lookRotation;
    Rigidbody myRigidbody;
    List<Vector3> positionHistory = new List<Vector3>();
    public float turnSpeed;
    public float moveSpeed;
    public float delay;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigidbody = GetComponent<Rigidbody>();
        StartCoroutine(FollowTarget());


    }

    IEnumerator FollowTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            Vector3 targetPosition = player.transform.position;
            Vector3 currentPosition = transform.position;
            Vector3 direction = (targetPosition - currentPosition).normalized;
            Vector3 newPosition = currentPosition + direction * moveSpeed * Time.deltaTime;

            transform.position = newPosition;
        }
    }
}
