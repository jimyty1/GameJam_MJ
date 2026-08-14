using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health = 5;

    private ZombieSpawner Spawner;

    [SerializeField]
    private TextMeshProUGUI HealthBar;


    private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                health -=1 ;
                this.HealthBar.SetText("{0}/5", health);
            }
            if(health <= 0)
        {
            Destroy(this.gameObject);
            Spawner.UpdateEnemyCount();
        }
        }
}
