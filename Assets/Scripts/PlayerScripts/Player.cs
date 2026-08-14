using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour

{
[SerializeField]
private int health = 10;
[SerializeField]
private int maxHealth = 10;
[SerializeField]
private TextMeshProUGUI HealthBar;


    private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Zombie"))
            {
                health -=1 ;
                this.HealthBar.SetText("{0}/{1}", health,maxHealth);
            }
            if(health <= 0)
            {
                RestartGame();
            }
        }
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
