using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private List<GameObject> spawnableEnemiesList;

    private static Dictionary<GameObject, EnemyController> currentEnemyList = new Dictionary<GameObject, EnemyController>();

    [SerializeField]
    private float spawnInterval = 1f;
    private float lastSpawnTime = 0f;

    [SerializeField]
    private int enemyMaxCount = 20;

    private void Awake()
    {
        currentEnemyList.Clear();
    }

    void Update()
    {
        if (!GameSettings.singleton.isGameStarted || GameSettings.singleton.isGamePaused)
            return;

        lastSpawnTime += Time.deltaTime;
        if (currentEnemyList.Count < enemyMaxCount && lastSpawnTime > spawnInterval)
        {
            SpawnEnemy();
        }
    }

    public static (GameObject, float) MostNearEnemy()
    {
        float nearestDistance = float.MaxValue;
        GameObject nearestObject = null;
        foreach (var item in currentEnemyList)
        {
            //float dist = item.Value.GetDistanceBetweenTarget();
            float dist = item.Value.GetDistanceBetweenTarget();
            if (dist < nearestDistance)
            {
                nearestDistance = dist;
                nearestObject = item.Key;
            }
        }
        return (nearestObject, nearestDistance);
    }

    private void SpawnEnemy()
    {
        lastSpawnTime = 0f;
        GameObject go;
        go = Instantiate(spawnableEnemiesList[Random.Range(0, spawnableEnemiesList.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity) as GameObject;
        go.transform.parent = transform;
        currentEnemyList.Add(go, go.GetComponent<EnemyController>());
    }

    public static void DestroyEnemy(GameObject enemy)
    {
        InGameAudioManager.singleton.PlayAudio(InGameAudioManager.AudioType.death);
        GameSettings.singleton.KilledEnemy();
        currentEnemyList[enemy]._animator.SetBool("die", true);
        currentEnemyList[enemy]._enemyCollider.enabled = false;
        currentEnemyList[enemy].isDead = true;
        currentEnemyList[enemy]._agent.enabled = false;
        currentEnemyList.Remove(enemy);
        Destroy(enemy, 2f);
    }
}
