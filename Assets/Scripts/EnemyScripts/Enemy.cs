using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    ZombieSpawner Spawner;
    private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(this.gameObject);
                Spawner.UpdateEnemyCount();
            }
        }
    public void Spawn()
    {
        GameObject Zombie = Instantiate(this.gameObject);
    }
}
