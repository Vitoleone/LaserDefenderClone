using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad());
    }

    public void LoadLevel1()
    {

        SceneManager.LoadScene("Level1");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game Over");
        
    }
}
