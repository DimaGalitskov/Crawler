using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    CrawlerInput inputActions;
    Rigidbody myRigidbody;
    Vector3 moveDirection;
    Vector3 startLocation;
    Quaternion lookRotation;
    List<Vector3> positionHistory = new List<Vector3>();
    List<GameObject> trailerChain = new List<GameObject>();
    public float turnSpeed;
    public float moveSpeed;
    public int gap;
    public int initialGap;
    public float minTurnSpeed;
    public GameObject trailer;
    public ParticleSystem particle;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        inputActions = new CrawlerInput();
        inputActions.Player.Move.started += MoveInput;
        inputActions.Player.Move.performed += MoveInput;
        inputActions.Player.Move.canceled += MoveInput;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        startLocation = transform.position;
        myRigidbody.AddRelativeForce(Vector3.forward * 5, ForceMode.Impulse);
    }

    private void Update()
    {
        Push();
        Turn();
        UpdateChain();
    }

    void MoveInput(InputAction.CallbackContext context)
    {
        moveDirection = new Vector3(0, context.ReadValue<Vector2>().x, 0);
        if (moveDirection != Vector3.zero)
        {
            lookRotation = Quaternion.LookRotation(moveDirection);
        }
    }

    void Push()
    {
        myRigidbody.AddRelativeForce(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void Turn()
    {
        float currentSpeed = Vector3.Project(myRigidbody.velocity, myRigidbody.transform.forward).magnitude;
        float turnSpeedModifier = Mathf.Clamp(currentSpeed / minTurnSpeed, 0, 1);
        Vector3 torque = moveDirection * turnSpeed * turnSpeedModifier * Time.deltaTime;
        myRigidbody.AddRelativeTorque(torque);
    }

    void UpdateChain()
    {
        positionHistory.Insert(0, transform.position);
        int index = 0;
        foreach (var part in trailerChain)
        {
            Vector3 point = positionHistory[Mathf.Min(initialGap + index * gap, positionHistory.Count - 1)];
            part.SendMessage("MoveTrailer", point);
            index++;
        }
    }

    void GrowChain()
    {
        int index = trailerChain.Count + 1;
        Vector3 point = positionHistory[Mathf.Min(initialGap + index * gap, positionHistory.Count - 1)];
        GameObject part = Instantiate(trailer, point, trailer.transform.rotation);
        trailerChain.Add(part);
    }

    void CutChain()
    {
        GameObject part = trailerChain[trailerChain.Count - 1];
        trailerChain.Remove(part);
        Destroy(part);
    }

    void ObstacleCollision()
    {
        SnakeDead();
        var thisParticle = Instantiate(particle, transform.position, particle.transform.rotation);
        Destroy(thisParticle, 1);
        Destroy(gameObject);
    }

    void SnakeDead()
    {
        int index = 0;
        foreach (var part in trailerChain)
        {
            part.SendMessage("SnakeDead");
            index++;
        }
    }
}
