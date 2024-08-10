using System.Collections;
using UnityEngine;

public class Spanwner : MonoBehaviour {
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int waves, enemiesPerWave;
    [SerializeField] private float timeBetweenWaves, timeBetweenSpawns;

    void Start() {
        StartCoroutine(Spawn());
    }

    public void StopSpawn() {
        StopAllCoroutines();
    }

    private IEnumerator Spawn() {
        for (int i = 0; i < waves; i++) {
            for (int j = 0; j < enemiesPerWave; j++) {
                yield return new WaitForSeconds(timeBetweenSpawns);
                
                int randomIndex = Random.Range(0, spawnPoints.Length);
                
                Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);

                GameManager.Instance.IncreaseEnemiesLeft();
            }
            
            if (i < waves - 1) {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        GameManager.Instance.SetAllWavesSpawmed();
    }
}