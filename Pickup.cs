using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float power;
    public float coneAngle;
    GameObject player;
    GameObject gameController;
    Rigidbody myRigidBody;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        myRigidBody = GetComponent<Rigidbody>();
        ThrowSelf();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("GrowChain");
            Destroy(gameObject);
        }
    }

    private void ThrowSelf()
    {
        var randomPower = Random.Range(0.5f, 1.5f);
        Vector3 originalDirection = Vector3.forward + Vector3.up * 0.5f;
        Vector3 randomDirectionWithinCone = AddRandomDirectionWithinCone(originalDirection, coneAngle);
        Vector3 direction = randomDirectionWithinCone * power * randomPower;
        myRigidBody.AddRelativeForce(direction, ForceMode.Impulse);
    }

    public static Vector3 AddRandomDirectionWithinCone(Vector3 originalDirection, float coneAngle)
    {
        // Clamp the cone angle to a valid range (0 to 180 degrees)
        coneAngle = Mathf.Clamp(coneAngle, 0, 180);

        // Generate a random angle within the cone angle range
        float randomAngle = Random.Range(0, coneAngle);

        // Generate a random rotation axis perpendicular to the original direction
        Vector3 randomRotationAxis = Vector3.Cross(originalDirection, Random.insideUnitSphere).normalized;

        // Create a rotation around the random rotation axis with the random angle
        Quaternion randomRotation = Quaternion.AngleAxis(randomAngle, randomRotationAxis);

        // Apply the random rotation to the original direction to get the new direction within the cone
        Vector3 newDirection = randomRotation * originalDirection;

        return newDirection;
    }
}
