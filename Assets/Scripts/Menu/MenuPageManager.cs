using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPageManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
