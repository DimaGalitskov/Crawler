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
    Quaternion lookRotation;
    public float turnSpeed;
    public float moveSpeed;
    List<Vector3> positionHistory = new List<Vector3>();
    public int gap = 10;
    public GameObject trailer;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        inputActions = new CrawlerInput();
        inputActions.Player.Move.started += MoveInput;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        Vector3 rbForward = myRigidbody.transform.forward;
        Vector3 torque = Vector3.Cross(rbForward, moveDirection ) * turnSpeed * Time.deltaTime;
        myRigidbody.AddRelativeForce(Vector3.forward * moveSpeed * Time.deltaTime);
        myRigidbody.AddTorque(torque);

        positionHistory.Insert(0, transform.position);
        Vector3 point = positionHistory[Mathf.Min(gap, positionHistory.Count - 1)];
        trailer.SendMessage("MoveTrailer", point);
    }

    void MoveInput(InputAction.CallbackContext context)
    {
        moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        if (moveDirection != Vector3.zero)
        {
            lookRotation = Quaternion.LookRotation(moveDirection);
        }
    }
}
