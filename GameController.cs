using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject pickup;
    public GameObject target;
    public TextMeshProUGUI playerScoreUI;
    public float spawnRangeX;
    public float spawnRangeZ;
    public float spawnSize;
    int playerScore = 0;

    private void Start()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        var step = 10;
        playerScore += step;
        playerScoreUI.SetText("Score: " + playerScore);
    }

    void SpawnReward(GameObject reward)
    {
        Vector3 position = FindFreePosition(Vector3.zero, spawnRangeX, spawnRangeZ, spawnSize, 10);
        Instantiate(reward, position, reward.transform.rotation);
    }


    private Vector3 GenerateRandomPosition(Vector3 center, float rangeX, float rangeZ)
    {
        float randomX = Random.Range(center.x - rangeX / 2, center.x + rangeX / 2);
        float randomZ = Random.Range(center.z - rangeZ / 2, center.z + rangeZ / 2);

        return new Vector3(randomX, center.y, randomZ);
    }


    private bool IsPositionFree(Vector3 position, float checkRadius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius);

        if (colliders.Length == 0)
        {
            return true;
        }

        return false;
    }


    private Vector3 FindFreePosition(Vector3 center, float rangeX, float rangeZ, float checkRadius, int maxAttempts)
    {
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector3 randomPosition = GenerateRandomPosition(center, rangeX, rangeZ);

            if (IsPositionFree(randomPosition, checkRadius))
            {
                return randomPosition;
            }

            attempts++;
        }

        return Vector3.zero; // Return Vector3.zero if no free position is found
    }
}
