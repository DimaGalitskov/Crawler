using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject ball;

    private void Start()
    {
        StartCoroutine(ThrowBall());
    }

    IEnumerator ThrowBall()
    {
        while (true)
        {
            Instantiate(ball, transform.position, transform.rotation);
            yield return new WaitForSeconds(1);
        }
    }
}
