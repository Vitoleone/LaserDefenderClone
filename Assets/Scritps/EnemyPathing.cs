using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    
     WaveConfig waveConfig;
    int startingWaypointIndex = 0;
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;	
    }


    private void Move()
    {
        

        if (startingWaypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[startingWaypointIndex].transform.position;
            var frameMoveSpeed = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, frameMoveSpeed);

            if (transform.position == targetPosition)
            {
                startingWaypointIndex++;
            }

        }

        else
        {
            Destroy(gameObject);
        }

       

    }
}
