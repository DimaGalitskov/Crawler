using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject reward;

    void SpawnReward()
    {
        Vector3 position = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));
        Instantiate(reward, position, reward.transform.rotation);
    }
}
