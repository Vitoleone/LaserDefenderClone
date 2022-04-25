using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;
    [SerializeField] bool looping = false;
    

   IEnumerator Start()//startý Ienumerator cinsinden yazdýk
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }


    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
       for(int enemyCount = 0;enemyCount < waveConfig.GetEnemyNumber();enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemy(),
                        waveConfig.GetWaypoints()[0].transform.position,
                        Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }

     
    }
}
