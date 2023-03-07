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
    List<Vector3> positionHistory = new List<Vector3>();
    List<GameObject> trailerChain = new List<GameObject>();
    public float turnSpeed;
    public float moveSpeed;
    public int gap;
    public int initialGap;
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

    private void Start()
    {
        myRigidbody.AddRelativeForce(Vector3.forward * 5, ForceMode.Impulse);
        StartCoroutine(AddChain());
    }

    private void Update()
    {
        Vector3 rbForward = myRigidbody.transform.forward;
        Vector3 torque = Vector3.Cross(rbForward, moveDirection ) * turnSpeed * Time.deltaTime;
        myRigidbody.AddRelativeForce(Vector3.forward * moveSpeed * Time.deltaTime);
        myRigidbody.AddTorque(torque);

        positionHistory.Insert(0, transform.position);

        int index = 0;
        foreach (var part in trailerChain)
        {
            Vector3 point = positionHistory[Mathf.Min(initialGap + index * gap, positionHistory.Count - 1)];
            part.SendMessage("MoveTrailer", point);
            index++;
        }
    }

    void MoveInput(InputAction.CallbackContext context)
    {
        moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        if (moveDirection != Vector3.zero)
        {
            lookRotation = Quaternion.LookRotation(moveDirection);
        }
    }

    void GrowChain()
    {
        GameObject part = Instantiate(trailer);
        trailerChain.Add(part);
    }

    IEnumerator AddChain()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            GrowChain();
        }
    }
}
