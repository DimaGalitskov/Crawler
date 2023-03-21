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
    public ParticleSystem particle;

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

    void SnakeDead()
    {
        Push();
        StartCoroutine( Particle());
    }

    void Push()
    {
        myRigidbody.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse);
    }

    IEnumerator Particle()
    {
        yield return new WaitForSeconds(0.5f + Random.Range(0.5f, 1f));
        var thisParticle = Instantiate(particle, transform.position, particle.transform.rotation);
        Destroy(thisParticle, 1);
        Destroy(gameObject);
    }
}
