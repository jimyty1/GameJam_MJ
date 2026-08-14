using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour{
private static int enemyCount = 0;
private bool active = false;
[SerializeField]
private GameObject _zombiePrefab;

[SerializeField]
private float _minimumSpawnTime;
[SerializeField]
private float _maximumSpawnTime;
[SerializeField]
private float _timeUntilSpawn;

    private void Awake()
    {
        SetTimeUntilSpawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            active = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            active = false;
        }
    }

    private void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;
        if (active && _timeUntilSpawn <= 0)
        {
            Instantiate(_zombiePrefab, transform.position, quaternion.identity);
            SetTimeUntilSpawn();
        }
    }
    
    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }

    public void UpdateEnemyCount()
    {
        enemyCount--;
        // your condition here
        if (enemyCount <= 0)
        {
            Debug.Log("All enemies are dead");
            // call some method here
        }
    }

}
    
    
