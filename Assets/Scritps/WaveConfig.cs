using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawn = 0.5f;
    [SerializeField] float timeRandomFactor = 0.3f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] int numberOfEnemy = 5;
   
   
    public GameObject GetEnemy()
    {
        return enemyPrefab;
    }

    public List<Transform> GetWaypoints()
    {
        var waypoints = new List<Transform>();

        foreach(Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public float GetTimeBetweenSpawn()
    {
        return timeBetweenSpawn;
    }

    public float GetTimeRandomFactor()
    {
        return timeRandomFactor;
    }

    public int GetEnemyNumber()
    {
        return numberOfEnemy;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
